using SciencePad.Visuals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciencePad.Scenes
{
    public class SineScene : VisualScene
    {
        public VisualCoordinate Coordinate { get; private set; }

        public SineScene()
        {
            this.Coordinate = new VisualCoordinate();
            this.VisualList.Add(this.Coordinate);
        }
    }
}
