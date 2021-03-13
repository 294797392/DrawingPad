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
    public class DrawableVisualLayer : Canvas
    {
        #region 实例变量

        private List<DependencyObject> visualHits;

        private Point previousPosition;

        private DrawableVisual selectedVisual;
        private DrawableVisual mouseHoveredVisual;
        private DrawableLine resizingLine;

        private DrawableState drawableState;

        #endregion

        #region 属性

        /// <summary>
        /// 所有图形集合
        /// </summary>
        public ObservableCollection<DrawableVisual> VisualList { get; private set; }

        protected override int VisualChildrenCount => this.VisualList.Count;

        #endregion

        #region 构造方法

        public DrawableVisualLayer()
        {
            this.UseLayoutRounding = true;
            this.SnapsToDevicePixels = true;
            this.Background = Brushes.White;

            this.drawableState = DrawableState.Idle;

            this.visualHits = new List<DependencyObject>();
            this.VisualList = new ObservableCollection<DrawableVisual>();
        }

        #endregion

        #region 构造方法

        public DrawableVisual DrawVisual(GraphicsBase graphics)
        {
            DrawableVisual visual = DrawableVisualFactory.Create(graphics);
            this.VisualList.Add(visual);

            this.AddVisualChild(visual);    // 该函数只会把DrawableVisual和DrawableVisualLayer关联起来，在渲染的时候并不会真正渲染。关联的目的是为了做命中测试（HitTest）。

            visual.Render();

            return visual;
        }

        #endregion

        #region 实例方法

        private DrawableVisual HitTestFirstVisual(Point hitPoint)
        {
            VisualTreeHelper.HitTest(this, null, this.HitTestResultCallback, new PointHitTestParameters(hitPoint));

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
                Rect boundary = selectedVisual.GetCircleHandleBounds(i);
                if (boundary.Contains(cursorPosition))
                {
                    this.Cursor = Cursors.Cross;
                    return;
                }
            }

            for (int i = 0; i < selectedVisual.RectangleHandles; i++)
            {
                Rect boundary = selectedVisual.GetRectangleHandleBounds(i);
                if (boundary.Contains(cursorPosition))
                {
                    this.Cursor = Cursors.Hand;
                    return;
                }
            }

            this.Cursor = Cursors.Arrow;
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

            Point cursorPosition = e.GetPosition(this);

            DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
            if (visualHit == null)
            {
                return;
            }

            this.ProcessSelectedVisualChanged(this.selectedVisual, visualHit);
            this.selectedVisual = visualHit;
            this.previousPosition = cursorPosition;

            for (int i = 0; i < visualHit.CircleHandles; i++)
            {
                Rect bounds = visualHit.GetCircleHandleBounds(i);
                if (bounds.Contains(cursorPosition))
                {
                    this.drawableState = DrawableState.Resize;
                    GraphicsBase graphics = new GraphicsLine()
                    {
                        StartPoint = new Point(bounds.TopLeft.X + bounds.Width / 2, bounds.TopLeft.Y + bounds.Height / 2)
                    };
                    this.resizingLine = this.DrawVisual(graphics) as DrawableLine;
                    return;
                }
            }

            this.drawableState = DrawableState.DragDrop;
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

                        DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
                        if (visualHit != null)
                        {
                            visualHit.IsMouseHover = true;
                            visualHit.Render();
                            this.mouseHoveredVisual = visualHit;
                        }
                        else
                        {
                            if (this.mouseHoveredVisual != null)
                            {
                                this.mouseHoveredVisual.IsMouseHover = false;
                                this.mouseHoveredVisual.Render();
                                this.mouseHoveredVisual = null;
                            }
                        }

                        #endregion

                        // 当存在选中的图形的时候，处理鼠标状态
                        this.ProcessSelectedVisualCursor(this.selectedVisual, cursorPosition);

                        break;
                    }

                case DrawableState.DragDrop:
                    {
                        double x = cursorPosition.X - this.previousPosition.X;
                        double y = cursorPosition.Y - this.previousPosition.Y;

                        this.selectedVisual.UpdatePosition(x, y);
                        this.selectedVisual.Render();

                        this.previousPosition = cursorPosition;

                        break;
                    }

                case DrawableState.Resize:
                    {
                        DrawableVisual visualHit = this.HitTestFirstVisual(cursorPosition);
                        this.resizingLine.Update(this.VisualList, this.selectedVisual, visualHit, cursorPosition);
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
                case DrawableState.DragDrop:
                    {
                        break;
                    }

                case DrawableState.Idle:
                    {
                        break;
                    }

                case DrawableState.Resize:
                    {
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }

            this.drawableState = DrawableState.Idle;
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
