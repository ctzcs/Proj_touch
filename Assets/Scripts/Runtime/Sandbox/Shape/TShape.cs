using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


    public class TShape:MyCustomShape
    {
        /// <summary>
        /// T只有一个唯一的高度
        /// </summary>
        public float height = 2;
        public override List<Vector2> CreateShape()
        {
            List<Vector2> points = new List<Vector2>();//这里必然产生大量GC，可恶啊！
            
            points.Add(new Vector2(-widthLeft,1*height/2));
            points.Add(new Vector2(widthRight,1*height/2));
            points.Add(new Vector2(widthRight,height/2 - thickness));
            points.Add(new Vector2(thickness/2,height/2 - thickness));
            points.Add(new Vector2(thickness/2,-height/2));
            points.Add(new Vector2(-thickness/2,-height/2));
            points.Add(new Vector2(-thickness/2,height/2 - thickness));
            points.Add(new Vector2(-widthLeft,height/2 - thickness));
            return points;
        }

        public void Start()
        {
            polygon.SetPoints(CreateShape());
        }

        public override void UpdateShapePoints()
        {
            polygon.SetPoints(CreateShape());
        }
    }
