using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public abstract class GraphicsBase
    {
        #region 公开属性

        public abstract GraphicsType Type { get; }

        /// <summary>
        /// 图形的ID
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
        [JsonProperty("Angle")]
        public double Angle { get; set; }

        /// <summary>
        /// 图形上显示的文本
        /// </summary>
        [JsonProperty("Text")]
        public string Text { get; set; }

        #endregion

        #region 抽象方法

        /// <summary>
        /// 对图形进行平移操作
        /// </summary>
        /// <param name="offsetX">x偏移量</param>
        /// <param name="offsetY">y偏移量</param>
        public abstract void Translate(double offsetX, double offsetY);

        /// <summary>
        /// 对图形进行调整大小的操作
        /// </summary>
        /// <param name="vertexPos">被调整大小的顶点位置</param>
        /// <param name="vertexPoint">顶点的新位置</param>
        public abstract void Resize(GraphicsVertexPosition vertexPos, Point oldPos, Point newPos);

        public abstract Point GetResizeHandle(int index);

        public abstract Point GetConnectionHandle(int index);

        public abstract Point GetRotationHandle();

        #endregion
    }
}
