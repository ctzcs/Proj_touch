using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;
using UnityEngine.Experimental.GlobalIllumination;

public class Tanim : MonoBehaviour
{
    public Polygon m_Polygon;

    private void Start()
    {
        StartCoroutine(TYAnimation());
        StartCoroutine(TXAnimation());
    }
    private void Update()
    {
        
    }


    public IEnumerator TYAnimation()
    {

        while (m_Polygon.points[4].y > -3 && m_Polygon.points[5].y > -3)
        {
            m_Polygon.SetPointPosition(4, 
                new Vector2(m_Polygon.points[4].x, m_Polygon.points[4].y - 0.001f));
            m_Polygon.SetPointPosition(5,
                new Vector2(m_Polygon.points[5].x, m_Polygon.points[5].y - 0.001f));
            m_Polygon.SetPointPosition(0,
                new Vector2(m_Polygon.points[0].x, m_Polygon.points[0].y + 0.001f));
            m_Polygon.SetPointPosition(1,
                new Vector2(m_Polygon.points[1].x, m_Polygon.points[1].y + 0.001f));
            m_Polygon.SetPointPosition(2,
                new Vector2(m_Polygon.points[2].x, m_Polygon.points[2].y + 0.001f));
            m_Polygon.SetPointPosition(3,
                new Vector2(m_Polygon.points[3].x, m_Polygon.points[3].y + 0.001f));
            m_Polygon.SetPointPosition(6,
                new Vector2(m_Polygon.points[6].x, m_Polygon.points[6].y + 0.001f));
            m_Polygon.SetPointPosition(7,
                new Vector2(m_Polygon.points[7].x, m_Polygon.points[7].y + 0.001f));
            yield return null;
        }
    }

    public IEnumerator TXAnimation()
    {
        while (m_Polygon.points[1].x < 5 && m_Polygon.points[2].x < 5)
        {
            m_Polygon.SetPointPosition(1,
                new Vector2(m_Polygon.points[1].x + 0.005f, m_Polygon.points[1].y));
            m_Polygon.SetPointPosition(2,
                new Vector2(m_Polygon.points[2].x + 0.005f, m_Polygon.points[2].y));

            yield return null;
        }
    }

    
}
