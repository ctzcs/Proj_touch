using System.Collections.Generic;
using Shapes;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Sandbox.Shape
{
    public enum EMyShape
    {
        T,
        H,
    }
    public abstract class MyCustomShape:MonoBehaviour
    {
        public EMyShape myShape;
        /// <summary>
        /// 总共变化的次数
        /// </summary>
        public int tick = 7;
        /// <summary>
        /// 线的宽度
        /// </summary>
        public float thickness = 0.25f;
        /// <summary>
        /// 几何体的左右宽度
        /// </summary>
        public float widthLeft = 1;
        public float widthRight = 1;
        
        /// <summary>
        /// 集合图形的polygon
        /// </summary>
        public Polygon polygon;
        
        /// <summary>
        /// 存放变化帧的多边形点的序列
        /// </summary>
        public List<List<Vector2>> pointsSequences;

        public abstract List<Vector2> CreateTShape();
    }
}