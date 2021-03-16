using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingPad
{
    public static class Pens
    {
        private const int DefaultWidth = 2;

        public static readonly Pen Black = new Pen(Brushes.Black, DefaultWidth);

    }
}
