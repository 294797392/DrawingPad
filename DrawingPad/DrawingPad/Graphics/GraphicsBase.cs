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

        /// <summary>
        /// 图形类型
        /// </summary>
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
        /// 保存文本属性信息
        /// </summary>
        [JsonProperty("TextProperties")]
        public TextProperties TextProperties { get; set; }

        /// <summary>
        /// 连接点数量
        /// </summary>
        public int ConnectionHandles { get; set; }

        /// <summary>
        /// 缩放点的数量
        /// </summary>
        public int ResizeHandles { get; set; }

        #endregion

        #region 构造方法

        public GraphicsBase()
        {
            this.ID = Guid.NewGuid().ToString();
            this.TextProperties = new TextProperties();
            this.ConnectionHandles = 4;
            this.ResizeHandles = 4;
        }

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
        /// <param name="location">缩放的点的位置</param>
        /// <param name="resizePos">缩放的点的坐标</param>
        /// <param name="newPos">顶点的新位置</param>
        public abstract void Resize(ResizeLocations location, Point resizePos, Point newPos);

        public abstract Point GetResizeHandle(int index);

        /// <summary>
        /// 获取圆形连接点的边界框
        /// </summary>
        /// <returns></returns>
        public abstract Rect GetConnectorBounds(int index);

        /// <summary>
        /// 获取矩形拖拽点的边界框
        /// </summary>
        /// <param name="numHandle"></param>
        /// <returns></returns>
        public abstract Rect GetResizeHandleBounds(int index);

        public abstract Point GetConnectionPoint(int index);

        public abstract Point GetRotationHandle();

        public abstract Rect GetBounds();

        /// <summary>
        /// 获取某个连接点的位置
        /// </summary>
        /// <param name="handleIndex">句柄索引</param>
        /// <returns></returns>
        public abstract ConnectionLocations GetConnectorLocation(int handleIndex);

        /// <summary>
        /// 获取某个缩放点的位置
        /// </summary>
        /// <param name="handlePoint"></param>
        /// <returns></returns>
        public abstract ResizeLocations GetResizeLocation(Point handlePoint);

        /// <summary>
        /// 测试某个坐标点是否是连接点
        /// </summary>
        /// <param name="hitTestPoint">要测试的点的坐标</param>
        /// <param name="connector">连接点的坐标</param>
        /// <param name="tolerance">容错值</param>
        /// <returns></returns>
        public ConnectionLocations HitTestConnectorLocation(Point hitTestPoint, out Point connector)
        {
            for (int i = 0; i < this.ConnectionHandles; i++)
            {
                Rect rect = this.GetConnectorBounds(i);
                if (rect.Contains(hitTestPoint))
                {
                    connector = rect.GetCenter();
                    return this.GetConnectorLocation(i);
                }
            }

            connector = hitTestPoint;

            return ConnectionLocations.Null;
        }

        /// <summary>
        /// 根据一个坐标点确定离这个坐标点最近的连接点索引
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool GetNearestConnectorHandle(Point point, out int handle, out Point handlePoint)
        {
            handle = -1;
            handlePoint = new Point();

            for (int i = 0; i < this.ConnectionHandles; i++)
            {
                handle = i;
                handlePoint = this.GetConnectionPoint(i);

                Rect rect = GraphicsUtility.MakeRect(handlePoint, PadContext.ConnectionLocationTolerance);
                if (rect.Contains(point))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
