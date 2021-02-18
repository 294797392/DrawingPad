using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SciencePad
{
    public static class PadUtility
    {
        public static Color RandomColor()
        {
            Random random = new Random();
            int r = random.Next(1, 254);
            int g = random.Next(1, 254);
            int b = random.Next(1, 254);
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

        public static Pen RandomColorPen(int thickness)
        {
            return new Pen(new SolidColorBrush(RandomColor()), thickness);
        }
    }
}
