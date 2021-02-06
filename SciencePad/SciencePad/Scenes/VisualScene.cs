using SciencePad.Visuals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SciencePad.Scenes
{
    /// <summary>
    /// 一个抽象的数学场景
    /// </summary>
    public class VisualScene : Canvas
    {
        #region 属性

        protected override int VisualChildrenCount { get { return this.VisualList.Count; } }

        public ObservableCollection<VisualGeometry> VisualList { get; private set; }

        #endregion

        #region 构造方法

        public VisualScene()
        {
            this.VisualList = new ObservableCollection<VisualGeometry>();
        }

        #endregion

        #region 实例方法

        protected override Visual GetVisualChild(int index)
        {
            return this.VisualList[index];
        }

        #endregion
    }
}