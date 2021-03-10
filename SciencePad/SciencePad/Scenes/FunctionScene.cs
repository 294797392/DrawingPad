using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SciencePad.Scenes
{
    public abstract class FunctionScene : CoordinateScene
    {
        #region 属性

        public ObservableCollection<IFunction> FunctionList { get; private set; }

        #endregion

        #region 构造方法

        public FunctionScene()
        {
            this.FunctionList = new ObservableCollection<IFunction>();
            this.FunctionList.CollectionChanged += this.FunctionList_CollectionChanged;
        }

        #endregion

        #region 抽象方法

        public abstract Geometry CreateFunctionGeometry(IFunction function);

        #endregion

        #region 重写方法

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            foreach (IFunction function in this.FunctionList)
            {
                this.DrawFunction(function, dc);
            }
        }

        #endregion

        #region 实例方法

        private void DrawFunction(IFunction function, DrawingContext dc)
        {
            Geometry funcGeometry = this.CreateFunctionGeometry(function);

            dc.DrawGeometry(null, function.LinePen, funcGeometry);
        }

        #endregion

        #region 事件处理器

        private void FunctionList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        #endregion
    }
}
