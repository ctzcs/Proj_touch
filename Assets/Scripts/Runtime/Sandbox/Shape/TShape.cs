using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Runtime.Sandbox.Shape
{
    public class TShape:MyCustomShape
    {
        /// <summary>
        /// T只有一个唯一的高度
        /// </summary>
        public float height = 2;
        public override List<Vector2> CreateTShape()
        {
            List<Vector2> points = new List<Vector2>();
            
            return points;
        }
    }
}