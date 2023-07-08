using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Sandbox.Shape
{
    
    public class HShape:MyCustomShape
    {
        /// <summary>
        /// 几何体的高度
        /// </summary>
        public float heightLeft = 2;
        public float heightRight = 2;
        
        public override List<Vector2> CreateTShape()
        {
            throw new System.NotImplementedException();
        }
    }
}