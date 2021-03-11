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

        private Point startOffset;
        private DrawableVisual visualHit;
        private Point startPosition;
        private Point previousPosition;
        private bool isDragging;

        #endregion

        #region 属性

        public ObservableCollection<DrawableVisual> VisualList { get; private set; }

        protected override int VisualChildrenCount => this.VisualList.Count;

        #endregion

        #region 构造方法

        public DrawableVisualLayer()
        {
            this.visualHits = new List<DependencyObject>();
            this.VisualList = new ObservableCollection<DrawableVisual>();
            this.Background = Brushes.White;
        }

        #endregion

        #region 构造方法

        public void DrawVisual(GraphicsBase graphics)
        {
            DrawableVisual visual = DrawableVisualFactory.Create(graphics);
            this.VisualList.Add(visual);
            
            this.AddVisualChild(visual);    // 该函数只会把DrawableVisual和DrawableVisualLayer关联起来，在渲染的时候并不会真正渲染。关联的目的是为了做命中测试（HitTest）。

            visual.Render();
        }

        #endregion

        #region 实例方法

        private DrawableVisual ProcessVisualHit(Point hitPoint)
        {
            VisualTreeHelper.HitTest(this, null, this.HitTestResultCallback, new PointHitTestParameters(hitPoint));

            if (this.visualHits.Count == 0)
            {
                return null;
            }

            // 被点击到的元素
            DrawableVisual visualHit = this.visualHits[0] as DrawableVisual;

            Console.WriteLine(visualHit.Name);

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

        #endregion

        #region 重写方法

        protected override Visual GetVisualChild(int index)
        {
            return this.VisualList[index];
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.startPosition = e.GetPosition(this);
            this.previousPosition = this.startPosition;

            DrawableVisual visualHit = this.ProcessVisualHit(this.startPosition);
            if (visualHit != null)
            {
                this.visualHit = visualHit;

                if (this.visualHit.Transform == null)
                {
                    TransformGroup transform = new TransformGroup();
                    transform.Children.Add(new TranslateTransform());
                    transform.Children.Add(new RotateTransform());
                    this.visualHit.Transform = transform;
                    this.startOffset = new Point(0, 0);
                }
                else
                {
                    TranslateTransform transform = (this.visualHit.Transform as TransformGroup).Children[0] as TranslateTransform;
                    this.startOffset = new Point(transform.X, transform.Y);
                }

                this.isDragging = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!this.isDragging)
            {
                return;
            }

            Point cursorPosition = e.GetPosition(this);

            TranslateTransform translate = null;

            if (this.visualHit.Transform == null)
            {
                TransformGroup transform = new TransformGroup();
                translate = new TranslateTransform();
                transform.Children.Add(translate);
                transform.Children.Add(new RotateTransform());
                this.visualHit.Transform = transform;
            }
            else
            {
                TransformGroup transform = this.visualHit.Transform as TransformGroup;
                translate = transform.Children[0] as TranslateTransform;
            }

            double x = cursorPosition.X - this.startPosition.X + this.startOffset.X;
            double y = cursorPosition.Y - this.startPosition.Y + this.startOffset.Y;

            Console.WriteLine("x = {0}, y = {1}", x, y);

            translate.BeginAnimation(TranslateTransform.XProperty, this.CreateTranslateAnimation(x), HandoffBehavior.Compose);
            translate.BeginAnimation(TranslateTransform.YProperty, this.CreateTranslateAnimation(y), HandoffBehavior.Compose);

            this.previousPosition = cursorPosition;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            this.isDragging = false;
            this.visualHit = null;
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
