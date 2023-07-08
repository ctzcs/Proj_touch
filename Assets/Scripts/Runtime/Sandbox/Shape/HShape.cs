using System.Collections.Generic;
using System.Threading;
using UnityEngine;


    
public class HShape:MyCustomShape
{
    /// <summary>
    /// 几何体的高度
    /// </summary>
    public float heightLeft = 2;
    public float heightRight = 2;
    
    public override List<Vector2> CreateShape()
    {
        List<Vector2> points = new List<Vector2>();//这里必然产生大量GC，可恶啊！
        points.Add(new Vector2(-widthLeft, heightLeft));
        points.Add(new Vector2(-widthLeft + thickness,heightLeft));
        points.Add(new Vector2(-widthLeft + thickness,thickness/2));
        
        points.Add(new Vector2(widthRight - thickness,thickness/2));
        points.Add(new Vector2(widthRight - thickness,heightRight));
        points.Add(new Vector2(widthRight,heightRight));
        
        points.Add(new Vector2(widthRight,-heightRight));
        points.Add(new Vector2(widthRight - thickness,-heightRight));
        points.Add(new Vector2(widthRight - thickness,-thickness/2));
        
        points.Add(new Vector2(-widthLeft + thickness,-thickness/2));
        points.Add(new Vector2(-widthLeft + thickness,-heightLeft));
        points.Add(new Vector2(-widthLeft, -heightLeft));
        return points;
    }
    public override void UpdateShapePoints()
    {
        polygon.SetPoints(CreateShape());
    }
}
