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

namespace DrawingPad.Layers
{
    /// <summary>
    /// 对图形进行拖拽，缩放等变换操作的层
    /// </summary>
    public class DrawingCanvas : DrawableVisualLayer
    {
        #region 实例变量

        private Dictionary<GraphicsType, DrawableVisual> drawableMap;

        private DrawableVisual drawableVisual;
        private RotateTransform rotateTransform;
        private TranslateTransform translateTransform;

        private Point startPosition;
        private Point currentPosition;

        #endregion

        #region 依赖属性

        public GraphicsBase DrawingGraphics
        {
            get { return (GraphicsBase)GetValue(DrawingGraphicsProperty); }
            set { SetValue(DrawingGraphicsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DrawingGeometry.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawingGraphicsProperty =
            DependencyProperty.Register("DrawingGraphics", typeof(GraphicsBase), typeof(DrawingCanvas), new PropertyMetadata(null, DrawingGraphicsPropertyChangedCallback));

        private static void DrawingGraphicsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DrawingCanvas canvas = d as DrawingCanvas;
            canvas.OnDrawingGraphicsChanged(e.OldValue as GraphicsBase, e.NewValue as GraphicsBase);
        }

        #endregion

        #region 构造方法

        public DrawingCanvas()
        {
            this.drawableMap = new Dictionary<GraphicsType, DrawableVisual>();
            this.translateTransform = new TranslateTransform();
            this.rotateTransform = new RotateTransform();
        }

        #endregion

        #region 实例方法

        private DrawableVisual GetDrawableVisual(GraphicsBase graphics)
        {
            DrawableVisual visual;
            if (!this.drawableMap.TryGetValue(graphics.Type, out visual))
            {
                visual = DrawableVisualFactory.Create(graphics);
                this.drawableMap[graphics.Type] = visual;
            }
            return visual;
        }

        #endregion

        #region 重写事件

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.drawableVisual;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            this.currentPosition = e.GetPosition(this);

            this.translateTransform.X = this.currentPosition.X - this.startPosition.X;
            this.translateTransform.Y = this.currentPosition.Y - this.startPosition.Y;
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            this.startPosition = e.GetPosition(this);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
        }

        #endregion

        #region 依赖属性回调

        private void OnDrawingGraphicsChanged(GraphicsBase oldGraphics, GraphicsBase newGraphics)
        {
            this.drawableVisual = DrawableVisual.Null;

            if (newGraphics != null)
            {
                this.drawableVisual = this.GetDrawableVisual(newGraphics);

                this.InvalidateVisual();
            }
        }

        #endregion
    }
}
