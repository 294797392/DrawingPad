using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public class GraphicsRectangle : GraphicsBase
    {
        public const int DefaultWidth = 100;
        public const int DefaultHeight = 100;

        public override GraphicsType Type { get { return GraphicsType.Rectangle; } }

        /// <summary>
        /// 左上角顶点的X
        /// </summary>
        public double Point1X { get; set; }

        /// <summary>
        /// 左上角顶点的Y
        /// </summary>
        public double Point1Y { get; set; }

        /// <summary>
        /// 矩形宽度
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 矩形高度
        /// </summary>
        public double Height { get; set; }

        public override void Translate(double offsetX, double offsetY)
        {
            this.Point1X += offsetX;
            this.Point1Y += offsetY;
        }

        public override void Resize(GraphicsVertexLocation vertex, Point oldPos, Point newPos)
        {
            switch (vertex)
            {
                case GraphicsVertexLocation.TopLeft:
                    {
                        double targetWidth = this.Width - (newPos.X - this.Point1X);
                        double targetHeight = this.Height - (newPos.Y - this.Point1Y);

                        if (targetWidth < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Width = targetWidth;
                            this.Point1X = newPos.X;
                        }

                        if (targetHeight < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Height = targetHeight;
                            this.Point1Y = newPos.Y;
                        }

                        break;
                    }

                case GraphicsVertexLocation.TopRight:
                    {
                        double targetWidth = newPos.X - this.Point1X;
                        double targetHeight = this.Height - (newPos.Y - this.Point1Y);

                        if (targetWidth < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Width = targetWidth;
                        }

                        if (targetHeight < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Height = targetHeight;
                            this.Point1Y = newPos.Y;
                        }

                        break;
                    }

                case GraphicsVertexLocation.BottomLeft:
                    {
                        double targetWidth = this.Width - (newPos.X - this.Point1X);
                        double targetHeight = newPos.Y - this.Point1Y;

                        if (targetWidth < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Width = targetWidth;
                            this.Point1X = newPos.X;
                        }

                        if (targetHeight < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Height = targetHeight;
                        }

                        break;
                    }

                case GraphicsVertexLocation.BottomRight:
                    {
                        double targetWidth = newPos.X - this.Point1X;
                        double targetHeight = newPos.Y - this.Point1Y;

                        if (targetWidth < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Width = targetWidth;
                        }

                        if (targetHeight < PadContext.MinimalVisualSize)
                        {
                        }
                        else
                        {
                            this.Height = targetHeight;
                        }

                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        public override Point GetConnectionHandle(int index)
        {
            if (index == 0)
            {
                // 左
                return new Point(this.Point1X, this.Point1Y + this.Height / 2);
            }
            else if (index == 1)
            {
                // 上
                return new Point(this.Point1X + this.Width / 2, this.Point1Y);
            }
            else if (index == 2)
            {
                // 右
                return new Point(this.Point1X + this.Width, this.Point1Y + this.Height / 2);
            }
            else if (index == 3)
            {
                // 下
                return new Point(this.Point1X + this.Width / 2, this.Point1Y + this.Height);
            }

            return new Point();
        }

        public override Point GetResizeHandle(int index)
        {
            if (index == 0)
            {
                // 左上角
                return new Point(this.Point1X, this.Point1Y);
            }
            else if (index == 1)
            {
                // 右上角
                return new Point(this.Point1X + this.Width, this.Point1Y);
            }
            else if (index == 2)
            {
                // 左下角
                return new Point(this.Point1X, this.Point1Y + this.Height);
            }
            else if (index == 3)
            {
                // 右下角
                return new Point(this.Point1X + this.Width, this.Point1Y + this.Height);
            }

            return new Point();
        }

        public override Point GetRotationHandle()
        {
            throw new NotImplementedException();
        }

        public override Rect GetBounds()
        {
            return new Rect()
            {
                Location = new Point(this.Point1X, this.Point1Y),
                Width = this.Width,
                Height = this.Height
            };
        }

        public Rect MakeRect()
        {
            return new Rect()
            {
                Location = new Point(this.Point1X, this.Point1Y),
                Height = this.Height,
                Width = this.Width
            };
        }
    }
}
