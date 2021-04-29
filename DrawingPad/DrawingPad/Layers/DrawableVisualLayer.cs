using DrawingPad.Visuals;
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

        /// <summary>
        /// 存储鼠标命中的元素列表
        /// </summary>
        private List<DependencyObject> visualHits;

        private VisualGraphics selectedVisual;
        private VisualGraphics previouseSelectedVisual;         // 上一个选中的图形
        private VisualGraphics previouseHoveredVisual;

        #region Connection状态

        /// <summary>
        /// 连接的第一个图形
        /// </summary>
        private VisualGraphics firstVisual;

        /// <summary>
        /// 连接的第二个图形
        /// </summary>
        private VisualGraphics secondVisual;

        /// <summary>
        /// 第一个连接图形的连接点
        /// </summary>
        private Point firstConnector;                           // 第一个连接点

        /// <summary>
        /// 正在连接的折线
        /// </summary>
        private VisualPolyline polyline;                // 正在连接的折线

        #endregion

        #region Translate状态

        /// <summary>
        /// 存储某个图形所关联的所有连线
        /// </summary>
        private Dictionary<string, List<GraphicsPolyline>> graphicsPolylines;

        /// <summary>
        /// 正在移动的图形
        /// </summary>
        private VisualGraphics translateVisual;

        /// <summary>
        /// 记录鼠标上次的位置
        /// </summary>
        private Point previousPosition;

        /// <summary>
        /// 图形在平移的时候所关联的所有连接线信息
        /// </summary>
        private List<GraphicsPolyline> associatedPolylines;

        #endregion

        #region Resize状态

        private ResizeLocations resizeLocation;                 // 起始缩放点的位置
        private Point resizeCenter;                             // 缩放点的中心坐标

        #endregion

        private Visuals.VisualState drawableState;

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

        public List<VisualGraphics> VisualList { get; private set; }

        #endregion

        #region 构造方法

        public DrawableVisualLayer()
        {
            this.UseLayoutRounding = true;
            this.SnapsToDevicePixels = true;
            this.Background = Brushes.White;

            this.drawableState = Visuals.VisualState.Idle;

            this.visualHits = new List<DependencyObject>();
            this.VisualList = new List<VisualGraphics>();

            this.graphicsPolylines = new Dictionary<string, List<GraphicsPolyline>>();
        }

        #endregion

        #region 公开接口

        public VisualGraphics DrawVisual(GraphicsBase graphics)
        {
            VisualGraphics visual = VisualFactory.Create(graphics);
            this.VisualList.Add(visual);

            this.AddVisualChild(visual);    // 该函数只会把DrawableVisual和DrawableVisualLayer关联起来，在渲染的时候并不会真正渲染。关联的目的是为了做命中测试（HitTest）。

            visual.Render();

            return visual;
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// 检测第一个被鼠标命中的元素
        /// </summary>
        /// <typeparam name="TExcluded">要排除在外的鼠标命中的元素的类型</typeparam>
        /// <param name="hitTestPos">鼠标坐标位置</param>
        /// <returns>第一个被鼠标命中的元素</returns>
        private VisualGraphics HitTestFirstVisual<TExcluded>(Point hitTestPos) where TExcluded : VisualGraphics
        {
            VisualTreeHelper.HitTest(this, null, this.HitTestResultCallback<TExcluded>, new PointHitTestParameters(hitTestPos));

            if (this.visualHits.Count == 0)
            {
                return null;
            }

            // 被点击到的元素
            VisualGraphics visualHit = this.visualHits[0] as VisualGraphics;

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
        private void ProcessSelectedVisualChanged(VisualGraphics previouseSelected, VisualGraphics selectedVisual)
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

        private void ProcessSelectedVisualCursor(VisualGraphics selectedVisual, Point cursorPosition)
        {
            if (selectedVisual == null)
            {
                this.Cursor = Cursors.Arrow;
                return;
            }

            for (int i = 0; i < selectedVisual.ConnectionHandles; i++)
            {
                Rect boundary = selectedVisual.GetConnectionHandleBounds(i);
                if (boundary.Contains(cursorPosition))
                {
                    this.Cursor = Cursors.Cross;
                    return;
                }
            }

            for (int i = 0; i < selectedVisual.ResizeHandles; i++)
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
        /// 当鼠标移动到图形上的时候，画Handle
        /// </summary>
        /// <param name="cursorPosition">鼠标坐标的位置</param>
        /// <returns>返回鼠标移动到的图形</returns>
        private VisualGraphics DrawHandleWhenMouseOverDrawable<ExcudedDrawable>(Point cursorPosition) where ExcudedDrawable : VisualGraphics
        {
            if (this.previouseHoveredVisual != null)
            {
                this.previouseHoveredVisual.IsDrawHandle = false;
                this.previouseHoveredVisual.Render();
                this.previouseHoveredVisual = null;
            }

            VisualGraphics visualHit = this.HitTestFirstVisual<ExcudedDrawable>(cursorPosition);
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

            return visualHit;
        }

        /// <summary>
        /// 保存图形里输入的文本
        /// </summary>
        private void StopVisualInputState(VisualGraphics visual)
        {
            if (this.drawableState != Visuals.VisualState.InputState)
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
        private void StartVisualInputState(VisualGraphics visual)
        {
            if (visual == null)
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

        /// <summary>
        /// 更新连接线
        /// </summary>
        /// <param name="polyline">要更新的折线</param>
        /// <param name="firstGraphics"></param>
        /// <param name="firstConnector">图形上的连接点</param>
        /// <param name="secondGraphics">连接到的图形</param>
        /// <param name="cursorPosition">当前鼠标的位置</param>
        /// <param name="connected">是否已连接</param>
        private void UpdatePolyline(VisualPolyline polyline, GraphicsBase firstGraphics, Point firstConnector, GraphicsBase secondGraphics, Point cursorPosition, out bool connected)
        {
            List<Point> pointList = GraphicsUtility.MakeConnectionPoints(firstGraphics, firstConnector, secondGraphics, cursorPosition, out connected);
            if (pointList != null)
            {
                polyline.Update(pointList);
            }
        }

        /// <summary>
        /// 获取某个图形关联的所有的折线
        /// </summary>
        /// <param name="graphics">要获取的图形</param>
        private List<GraphicsPolyline> GetAssociatedPolylines(GraphicsBase graphics)
        {
            if (graphics.Type == GraphicsType.Polyline)
            {
                return null;
            }

            List<GraphicsPolyline> result;
            if (!this.graphicsPolylines.TryGetValue(graphics.ID, out result))
            {
                // 先找到所有折线图形
                IEnumerable<GraphicsPolyline> polylines = this.VisualList.Select(v => v.Graphics).OfType<GraphicsPolyline>();

                // 从折线图形里筛选
                foreach (GraphicsPolyline polyline in polylines)
                {
                    if (polyline.AssociatedGraphics1 == graphics.ID || polyline.AssociatedGraphics2 == graphics.ID)
                    {
                        result.Add(polyline);
                    }
                }
            }

            return result;
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

            VisualGraphics visualHit = this.HitTestFirstVisual<ExcludedNullVisual>(cursorPosition);
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
                this.drawableState = Visuals.VisualState.Idle;

                return;
            }

            this.ProcessSelectedVisualChanged(this.selectedVisual, visualHit);
            this.selectedVisual = visualHit;
            this.previousPosition = cursorPosition;

            if (e.ClickCount == 2)
            {
                // 双击图形，那么进入编辑状态
                this.drawableState = Visuals.VisualState.InputState;
                this.StartVisualInputState(this.selectedVisual);
            }
            else
            {
                this.StopVisualInputState(this.previouseSelectedVisual);

                #region 判断是否点击了连接点

                for (int i = 0; i < visualHit.ConnectionHandles; i++)
                {
                    Rect bounds = visualHit.GetConnectionHandleBounds(i);
                    if (bounds.Contains(cursorPosition))
                    {
                        Point center = bounds.GetCenter();

                        ConnectionLocations location = visualHit.Graphics.GetConnectionLocation(cursorPosition);

                        this.drawableState = Visuals.VisualState.Connecting;
                        GraphicsPolyline graphics = new GraphicsPolyline()
                        {
                            //ConnectionPoint = center,
                            //StartConnectionLocation = location,
                            //StartVisual = visualHit
                        };
                        this.polyline = this.DrawVisual(graphics) as VisualPolyline;
                        this.firstConnector = center;
                        this.firstVisual = visualHit;
                        return;
                    }
                }

                #endregion

                #region 判断是否点击了Reisze点

                for (int i = 0; i < visualHit.ResizeHandles; i++)
                {
                    Rect bounds = visualHit.GetResizeHandleBounds(i);
                    if (bounds.Contains(cursorPosition))
                    {
                        this.resizeCenter = bounds.GetCenter();
                        this.resizeLocation = visualHit.Graphics.GetResizeLocation(this.resizeCenter);
                        this.drawableState = Visuals.VisualState.Resizing;
                        return;
                    }
                }

                #endregion

                #region 是平移状态

                // 获取当前被移动的图形所有的连接点信息

                this.associatedPolylines = this.GetAssociatedPolylines(visualHit.Graphics);
                if (associatedPolylines != null && associatedPolylines.Count > 0)
                {
                    Console.WriteLine("关联的折线有{0}个", this.associatedPolylines.Count);
                }

                this.translateVisual = visualHit;

                this.drawableState = Visuals.VisualState.Translate;

                #endregion
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point cursorPosition = e.GetPosition(this);

            switch (this.drawableState)
            {
                case Visuals.VisualState.Idle:
                    {
                        VisualGraphics visualHit = this.DrawHandleWhenMouseOverDrawable<VisualPolyline>(cursorPosition);

                        // 处理鼠标状态
                        this.ProcessSelectedVisualCursor(visualHit, cursorPosition);

                        break;
                    }

                case Visuals.VisualState.Translate:
                    {
                        double x = cursorPosition.X - this.previousPosition.X;
                        double y = cursorPosition.Y - this.previousPosition.Y;

                        this.translateVisual.Translate(x, y);

                        foreach (GraphicsPolyline polyline in this.associatedPolylines)
                        {
                            if (this.translateVisual.ID == polyline.AssociatedGraphics1)
                            {
                                // 起始点是translateVisual
                                VisualPolyline visualPolyline = this.VisualList.OfType<VisualPolyline>().FirstOrDefault(v => v.ID == polyline.ID);
                                bool connected;
                                //this.UpdatePolyline(this.translateVisual, this.translateVisual.Graphics, firstConnector, secondVisual.Graphics, secondConnector, out connected);
                            }
                            else if (this.translateVisual.ID == polyline.AssociatedGraphics2)
                            {
                                // 终结点是translateVisual
                            }
                        }

                        this.previousPosition = cursorPosition;

                        break;
                    }

                case Visuals.VisualState.Connecting:
                    {
                        // 检测鼠标是否在某个图形上面
                        VisualGraphics visualHit = this.DrawHandleWhenMouseOverDrawable<VisualPolyline>(cursorPosition);
                        if (visualHit != null && visualHit.Type != GraphicsType.Polyline)
                        {
                            this.secondVisual = visualHit;
                        }
                        else
                        {
                            // 如果当前鼠标下的元素和选中的是同一个元素，那么就表示没有要连接的元素
                        }

                        // 当前正在画的折线
                        GraphicsPolyline polyline = this.polyline.Graphics as GraphicsPolyline;

                        // 当鼠标在另外一个元素上的时候，说明此时两个图形被连接起来了
                        bool connected;
                        this.UpdatePolyline(this.polyline, this.firstVisual.Graphics, this.firstConnector, this.secondVisual == null ? null : this.secondVisual.Graphics, cursorPosition, out connected);
                        if (connected)
                        {
                            polyline.AssociatedGraphics1 = this.firstVisual.ID;
                            polyline.AssociatedGraphics2 = this.secondVisual.ID;
                        }
                        else
                        {
                            polyline.AssociatedGraphics1 = this.firstVisual.ID;
                            polyline.AssociatedGraphics2 = null;
                        }

                        break;
                    }

                case Visuals.VisualState.Resizing:
                    {
                        this.selectedVisual.Resize(this.resizeLocation, this.resizeCenter, cursorPosition);
                        break;
                    }

                case Visuals.VisualState.InputState:
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
                case Visuals.VisualState.Translate:
                    {
                        this.drawableState = Visuals.VisualState.Idle;
                        this.translateVisual = null;
                        this.associatedPolylines = null;
                        break;
                    }

                case Visuals.VisualState.Idle:
                    {
                        break;
                    }

                case Visuals.VisualState.Connecting:
                    {
                        this.drawableState = Visuals.VisualState.Idle;
                        this.firstVisual = null;
                        this.secondVisual = null;
                        break;
                    }

                case Visuals.VisualState.Resizing:
                    {
                        this.drawableState = Visuals.VisualState.Idle;
                        break;
                    }

                case Visuals.VisualState.InputState:
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
        private HitTestResultBehavior HitTestResultCallback<TExcluded>(HitTestResult result)
        {
            if (result.VisualHit is TExcluded)
            {
                return HitTestResultBehavior.Continue;
            }

            if (result.VisualHit is VisualGraphics)
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
