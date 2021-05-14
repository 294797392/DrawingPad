using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public static class GraphicsFactory
    {
        /// <summary>
        /// 创建一个图形实例
        /// </summary>
        /// <param name="center">图形的中心点</param>
        /// <param name="type">图形类型</param>
        /// <returns></returns>
        public static GraphicsBase Create(Point center, GraphicsType type)
        {
            switch (type)
            {
                case GraphicsType.Ellipse:
                    {
                        return new GraphicsEllipse() 
                        {
                            Width = GraphicsRectangle.DefaultWidth,
                            Height = GraphicsRectangle.DefaultHeight,
                            Point1X = center.X - GraphicsRectangle.DefaultWidth / 2,
                            Point1Y = center.Y - GraphicsRectangle.DefaultHeight / 2
                        };
                    }

                case GraphicsType.Rectangle:
                    {
                        return new GraphicsRectangle()
                        {
                            Width = GraphicsRectangle.DefaultWidth,
                            Height = GraphicsRectangle.DefaultHeight,
                            Point1X = center.X - GraphicsRectangle.DefaultWidth / 2,
                            Point1Y = center.Y - GraphicsRectangle.DefaultHeight / 2
                        };
                    }


                default:
                    throw new NotImplementedException();
            }
        }
    }
}
