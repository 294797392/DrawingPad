using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DrawingPad.Layers
{
    /// <summary>
    /// 棋盘格背景
    /// </summary>
    public class CheckerboardLayer : BackgroundLayer
    {
        private static readonly SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);
        private static readonly SolidColorBrush WhiteBrush = new SolidColorBrush(Colors.White);

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            double width = base.Width;
            double height = base.Height;

            int pixelPerGrid = 30;

            int numRow = (int)Math.Floor(height / pixelPerGrid);
            int numCol = (int)Math.Floor(width / pixelPerGrid);

            for (int row = 0; row < numRow; row++)
            {
                for (int col = 0; col < numCol; col++)
                {
                    SolidColorBrush brush = (row + col) % 2 == 0 ? BlackBrush : WhiteBrush;     // 背景颜色画刷

                    Rect grid = new Rect()
                    {
                        X = row * pixelPerGrid,
                        Y = col * pixelPerGrid,
                        Width = pixelPerGrid,
                        Height = pixelPerGrid
                    };

                    dc.DrawRectangle(brush, null, grid);
                }
            }

            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(5000);

            //    this.Dispatcher.Invoke(() =>
            //    {
            //        System.IO.FileStream fs = new System.IO.FileStream(@"out.png", System.IO.FileMode.Create);
            //        RenderTargetBitmap bmp = new RenderTargetBitmap((int)this.Width, (int)this.Height, 96d, 96d, PixelFormats.Pbgra32);
            //        bmp.Render(this);
            //        BitmapEncoder encoder = new PngBitmapEncoder();
            //        encoder.Frames.Add(BitmapFrame.Create(bmp));
            //        encoder.Save(fs);
            //        fs.Close();
            //    });
            //});
        }
    }
}
