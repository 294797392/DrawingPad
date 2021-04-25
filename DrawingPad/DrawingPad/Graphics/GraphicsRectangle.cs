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

        public override void Resize(ResizeLocations location, Point oldPos, Point newPos)
        {
            switch (location)
            {
                case ResizeLocations.TopLeft:
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

                case ResizeLocations.TopRight:
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

                case ResizeLocations.BottomLeft:
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

                case ResizeLocations.BottomRight:
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

        public override ConnectionLocations GetConnectionLocation(Point handlePoint)
        {
            Rect bounds = this.GetBounds();
            Point leftTop = bounds.TopLeft;

            if (handlePoint.X == leftTop.X && handlePoint.Y == leftTop.Y + bounds.Height / 2)
            {
                return ConnectionLocations.Left;
            }
            else if (handlePoint.X == leftTop.X + bounds.Width / 2 && handlePoint.Y == leftTop.Y)
            {
                return ConnectionLocations.Top;
            }
            else if (handlePoint.X == leftTop.X + bounds.Width / 2 && handlePoint.Y == leftTop.Y + bounds.Height)
            {
                return ConnectionLocations.Bottom;
            }
            else if (handlePoint.X == leftTop.X + bounds.Width && handlePoint.Y == leftTop.Y + bounds.Height / 2)
            {
                return ConnectionLocations.Right;
            }

            throw new NotImplementedException();
        }

        public override ResizeLocations GetResizeLocation(Point handlePoint)
        {
            Rect bounds = this.GetBounds();
            Point leftTop = bounds.TopLeft;

            if (handlePoint.X == leftTop.X && handlePoint.Y == leftTop.Y)
            {
                return ResizeLocations.TopLeft;
            }
            else if (handlePoint.X == bounds.TopRight.X && handlePoint.Y == bounds.TopRight.Y)
            {
                return ResizeLocations.TopRight;
            }
            else if (handlePoint.X == bounds.BottomLeft.X && handlePoint.Y == bounds.BottomLeft.Y)
            {
                return ResizeLocations.BottomLeft;
            }
            else if (handlePoint.X == bounds.BottomRight.X && handlePoint.Y == bounds.BottomRight.Y)
            {
                return ResizeLocations.BottomRight;
            }

            throw new NotImplementedException();
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
