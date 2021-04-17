using DrawingPad.Drawable;
using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DrawingPad.Layers
{
    public class DrawableVisualLayer : DrawingLayer
    {
        #region 类变量

        private static log4net.ILog logger = log4net.LogManager.GetLogger("DrawableVisualLayer");

        #endregion

        #region 实例变量

        private List<DependencyObject> visualHits;

        private Point previousPosition;

        private DrawableVisual selectedVisual;
        private DrawableVisual previouseSelectedVisual;         // 上一个选中的图形
        private DrawableVisual previouseHoveredVisual;

        private Point firstConnector;                       // 第一个连接点
        private DrawableConnectionLine connectionLine;

        private GraphicsVertexPosition vertexPos;               // 起始缩放点的位置
        private Point vertexCenter;                             // 调整大小的顶点的中心坐标

        private DrawableState drawableState;

        #endregion

        #region 属性

        public TextBox TextBoxEditor
        {
            get { return (TextBox)GetValue(TextBoxEditorProperty); }
            set { SetValue(TextBoxEditorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxEditor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxEditorProperty =
            DependencyProperty.Register("TextBoxEditor", typeof(TextBox), typeof(DrawableVisualLayer), new PropertyMetadata(null));

        protected override int VisualChildrenCount => this.VisualList.Count;

        public VisualCollection VisualList { get; private set; }

        #endregion

        #region 构造方法

        public DrawableVisualLayer()
        {
            this.UseLayoutRounding = true;
            this.SnapsToDevicePixels = true;
            this.Background = Brushes.White;

            this.drawableState = DrawableState.Idle;

            this.visualHits = new List<DependencyObject>();
            this.VisualList = new VisualCollection(this);
        }

        #endregion

        #region 公开接口

        public DrawableVisual DrawVisual(GraphicsBase graphics)
        {
            DrawableVisual visual = DrawableVisualFactory.Create(graphics);
            this.VisualList.Add(visual);

            //this.AddVisualChild(visual);    // 该函数只会把DrawableVisual和DrawableVisualLayer关联起来，在渲染的时候并不会真正渲染。关联的目的是为了做命中测试（HitTest）。

            visual.Render();

            return visual;
        }

        #endregion

        #region 实例方法

        private DrawableVisual HitTestFirstVisual(Point hitTestPos)
        {
            VisualTreeHelper.HitTest(this, null, this.HitTestResultCallback, new PointHitTestParameters(hitTestPos));

            if (this.visualHits.Count == 0)
            {
                return null;
            }

            // 被点击到的元素
            DrawableVisual visualHit = this.visualHits[0] as DrawableVisual;

            this.visualHits.Clear();

            return visualHit;
        }

        /// <summary>Helper to create the zoom double animation for scaling.</summary>
        /// <param name="toValue">Value to animate to.</param>
        /// <returns>Double animation.</returns>
        private DoubleAnimation CreateTranslateAnimation(double toValue)
        {
            var da = new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(0)));
            da.AccelerationRatio = 0.1;
            da.DecelerationRatio = 0.9;
            da.FillBehavior = FillBehavior.HoldEnd;
            da.Freeze();
            return da;
        }

        /// <summary>
        /// 当前选中的Visual改变的时候被调用
        /// 保证previouseSelected和selectedVisual都不为空
        /// </summary>
        /// <param name="previouseSelected"></param>
        /// <param name="selectedVisual"></param>
        private void ProcessSelectedVisualChanged(DrawableVisual previouseSelected, DrawableVisual selectedVisual)
        {
            if (previouseSelected == selectedVisual)
            {
                return;
            }

            if (previouseSelected != null)
            {
                if (previouseSelected != selectedVisual)
                {
                    previouseSelected.IsSelected = false;
                    previouseSelected.Render();

                    selectedVisual.IsSelected = true;
                    selectedVisual.Render();
                }
            }
            else
            {
                selectedVisual.IsSelected = true;
                selectedVisual.Render();
            }
        }

        private void ProcessSelectedVisualCursor(DrawableVisual selectedVisual, Point cursorPosition)
        {
            if (selectedVisual == null)
            {
                this.Cursor = Cursors.Arrow;
                return;
            }

            for (int i = 0; i < selectedVisual.CircleHandles; i++)
            {
                Rect boundary = selectedVisual.GetConnectionHandleBounds(i);
                if (boundary.Contains(cursorPosition))
                {
                    this.Cursor = Cursors.Cross;
                    return;
                }
            }

            for (int i = 0; i < selectedVisual.RectangleHandles; i++)
            {
                Rect boundary = selectedVisual.GetResizeHandleBounds(i);
                if (boundary.Contains(cursorPosition))
                {
                    this.Cursor = Cursors.Hand;
                    return;
                }
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 保存图形里输入的文本
        /// </summary>
        private void StopVisualInputState(DrawableVisual visual)
        {
            if (this.drawableState != DrawableState.InputState)
            {
                return;
            }

            if (visual == null)
            {
                return;
            }

            visual.Graphics.TextProperties.Text = this.TextBoxEditor.Text;
            visual.Render();    // 刷新文本信息
            this.TextBoxEditor.Text = null;
            this.TextBoxEditor.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 让一个图形进入编辑状态
        /// </summary>
        /// <param name="visual"></param>
        private void StartVisualInputState(DrawableVisual visual)
        {
            if(visual == null)
            {
                logger.WarnFormat("要编辑的图形为空");
                return;
            }

            Rect bounds = visual.GetTextBounds();
            this.TextBoxEditor.Width = bounds.Width;
            this.TextBoxEditor.Height = bounds.Height;
            Canvas.SetLeft(this.TextBoxEditor, bounds.TopLeft.X);
            Canvas.SetTop(this.TextBoxEditor, bounds.TopLeft.Y);
            this.TextBoxEditor.Text = visual.Graphics.TextProperties.Text;
            this.TextBoxEditor.Visibility = Visibility.Visible;
            this.TextBoxEditor.Focus();
        }

        #endregion

        #region 重写方法

        protected override Visual GetVisualChild(int index)
        {
            return this.VisualList[index];
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.previouseSelectedVisual = this.selectedVisual;

            Point cursorPosition = e.GetPosition(this);

            DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
            if (visualHit == null)
            {
                if (this.selectedVisual != null)
                {
                    this.selectedVisual.IsSelected = false;
                    this.selectedVisual.IsDrawHandle = false;
                    this.selectedVisual.Render();
                    this.selectedVisual = null;
                }

                // 停止编辑状态
                this.StopVisualInputState(this.previouseSelectedVisual);
                TextBoxEditor.Visibility = Visibility.Collapsed;

                // 重置到空闲状态
                this.drawableState = DrawableState.Idle;

                return;
            }

            this.ProcessSelectedVisualChanged(this.selectedVisual, visualHit);
            this.selectedVisual = visualHit;
            this.previousPosition = cursorPosition;

            if (e.ClickCount == 2)
            {
                // 双击图形，那么进入编辑状态
                this.drawableState = DrawableState.InputState;
                this.StartVisualInputState(this.selectedVisual);
            }
            else
            {
                this.StopVisualInputState(this.previouseSelectedVisual);

                this.drawableState = DrawableState.Translate;

                #region 判断是否点击了连接点

                for (int i = 0; i < visualHit.CircleHandles; i++)
                {
                    Rect bounds = visualHit.GetConnectionHandleBounds(i);
                    if (bounds.Contains(cursorPosition))
                    {
                        Point center = bounds.GetCenter();
                        GraphicsVertexPosition position = GraphicsUtility.GetVertex(visualHit, center);

                        this.drawableState = DrawableState.DrawConnectionLine;
                        GraphicsBase graphics = new GraphicsConnectionLine()
                        {
                            ConnectionPoint = center,
                            StartPointPosition = position,
                            StartVisual = visualHit
                        };
                        this.connectionLine = this.DrawVisual(graphics) as DrawableConnectionLine;
                        this.firstConnector = center;
                        return;
                    }
                }

                #endregion

                #region 判断是否点击了Reisze点

                for (int i = 0; i < visualHit.RectangleHandles; i++)
                {
                    Rect bounds = visualHit.GetResizeHandleBounds(i);
                    if (bounds.Contains(cursorPosition))
                    {
                        this.vertexCenter = bounds.GetCenter();
                        this.vertexPos = GraphicsUtility.GetVertex(visualHit, this.vertexCenter);
                        this.drawableState = DrawableState.Resizing;
                        return;
                    }
                }

                #endregion
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point cursorPosition = e.GetPosition(this);

            switch (this.drawableState)
            {
                case DrawableState.Idle:
                    {
                        #region 处理鼠标移动到图形上之后，图形显示连接点的逻辑

                        if (this.previouseHoveredVisual != null)
                        {
                            this.previouseHoveredVisual.IsDrawHandle = false;
                            this.previouseHoveredVisual.Render();
                            this.previouseHoveredVisual = null;
                        }

                        DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
                        if (visualHit == null)
                        {
                            //if (this.previouseHoveredVisual != null)
                            //{
                            //    this.previouseHoveredVisual.IsDrawHandle = false;
                            //    this.previouseHoveredVisual.Render();
                            //    this.previouseHoveredVisual = null;
                            //}
                        }
                        else
                        {
                            this.previouseHoveredVisual = visualHit;
                            this.previouseHoveredVisual.IsDrawHandle = true;
                            this.previouseHoveredVisual.Render();
                        }

                        #endregion

                        // 处理鼠标状态
                        this.ProcessSelectedVisualCursor(visualHit, cursorPosition);

                        break;
                    }

                case DrawableState.Translate:
                    {
                        double x = cursorPosition.X - this.previousPosition.X;
                        double y = cursorPosition.Y - this.previousPosition.Y;

                        this.selectedVisual.Translate(x, y);

                        this.previousPosition = cursorPosition;

                        break;
                    }

                case DrawableState.DrawConnectionLine:
                    {
                        List<Point> pointList = DrawableVisualUtility.GetConnectionPoints(this.selectedVisual, this.firstConnector, cursorPosition);
                        if (pointList == null)
                        {
                            return;
                        }
                        this.connectionLine.Update(pointList);

                        DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
                        if (visualHit != null)
                        {
                            Console.WriteLine(visualHit.Name);
                        }

                        break;
                    }

                case DrawableState.Resizing:
                    {
                        this.selectedVisual.Resize(this.vertexPos, this.vertexCenter, cursorPosition);
                        break;
                    }

                case DrawableState.InputState:
                    {
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            Point cursorPosition = e.GetPosition(this);

            switch (this.drawableState)
            {
                case DrawableState.Translate:
                    {
                        this.drawableState = DrawableState.Idle;
                        break;
                    }

                case DrawableState.Idle:
                    {
                        break;
                    }

                case DrawableState.DrawConnectionLine:
                    {
                        this.drawableState = DrawableState.Idle;
                        break;
                    }

                case DrawableState.Resizing:
                    {
                        this.drawableState = DrawableState.Idle;
                        break;
                    }

                case DrawableState.InputState:
                    {
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 事件处理器

        // If a child visual object is hit, toggle its opacity to visually indicate a hit.
        private HitTestResultBehavior HitTestResultCallback(HitTestResult result)
        {
            if (result.VisualHit is DrawableVisual)
            {
                this.visualHits.Add(result.VisualHit);

                // Stop the hit test enumeration of objects in the visual tree.
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }

        #endregion
    }
}
