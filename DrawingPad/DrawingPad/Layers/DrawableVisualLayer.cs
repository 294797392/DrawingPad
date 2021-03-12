﻿using DrawingPad.Drawable;
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
        private Point startPosition;

        private DrawableVisual selectedVisual;
        private DrawableVisual mouseHoveredVisual;

        private DrawableState drawableState;

        #endregion

        #region 属性

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

        public void DrawVisual(GraphicsBase graphics)
        {
            DrawableVisual visual = DrawableVisualFactory.Create(graphics);
            this.VisualList.Add(visual);

            this.AddVisualChild(visual);    // 该函数只会把DrawableVisual和DrawableVisualLayer关联起来，在渲染的时候并不会真正渲染。关联的目的是为了做命中测试（HitTest）。

            visual.Render();
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

        private void ProcessSelectedVisualChanged(DrawableVisual oldVisual, DrawableVisual selectedVisual)
        {
            if (oldVisual == selectedVisual)
            {
                return;
            }

            if (oldVisual != null)
            {
                if (oldVisual != selectedVisual)
                {
                    oldVisual.IsSelected = false;
                    oldVisual.Render();

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

            DrawableVisual visualHit = this.HitTestFirstVisual(this.startPosition);
            if (visualHit != null)
            {
                this.ProcessSelectedVisualChanged(this.selectedVisual, visualHit);
                this.selectedVisual = visualHit;

                if (this.selectedVisual.Transform == null)
                {
                    TransformGroup transform = new TransformGroup();
                    transform.Children.Add(new TranslateTransform());
                    transform.Children.Add(new RotateTransform());
                    this.selectedVisual.Transform = transform;
                    this.startOffset = new Point(0, 0);
                }
                else
                {
                    TranslateTransform transform = (this.selectedVisual.Transform as TransformGroup).Children[0] as TranslateTransform;
                    this.startOffset = new Point(transform.X, transform.Y);
                }

                this.drawableState = DrawableState.DragDrop;
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
                    }
                    break;

                case DrawableState.DragDrop:
                    {
                        TranslateTransform translate = (this.selectedVisual.Transform as TransformGroup).Children[0] as TranslateTransform;

                        double x = cursorPosition.X - this.startPosition.X + this.startOffset.X;
                        double y = cursorPosition.Y - this.startPosition.Y + this.startOffset.Y;

                        translate.BeginAnimation(TranslateTransform.XProperty, this.CreateTranslateAnimation(x), HandoffBehavior.Compose);
                        translate.BeginAnimation(TranslateTransform.YProperty, this.CreateTranslateAnimation(y), HandoffBehavior.Compose);
                    }
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

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
