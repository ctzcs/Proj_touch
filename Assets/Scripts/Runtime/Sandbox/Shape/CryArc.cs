using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Shapes;
using Unity.VisualScripting;
using UnityEngine;

public class CryArc : MonoBehaviour
{
    public float radius;
    
    [Range(0,1)]
    public float range;
    
    public float recordX;
    public float rangeDeltaSpeed;

    /// <summary>
    /// x正轴逆时针开始算的角度,结束的角度
    /// </summary>
    public float endDegree;
    /// <summary>
    /// 开始的角度
    /// </summary>
    public float startDegree;

    public float controlDegree;
    /// <summary>
    /// 填充物
    /// </summary>
    public Disc fill;
    /// <summary>
    /// 背景
    /// </summary>
    public Transform bg;
    /// <summary>
    /// handle
    /// </summary>
    public Transform handle;
    
    /*[HideInInspector]
    public RaycastHit2D[] hitInfo;*/
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //用射线检测到range变化
        if (Input.GetMouseButton(0))
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D info = Physics2D.Raycast(point, Vector2.zero,0.1f);
            point.z = 0;
            
            if (info.collider.CompareTag("Handle"))
            {
                var florence = Mathf.Abs(point.x - recordX);
                if (florence > 0.001f)
                {
                    var localPoint = transform.InverseTransformPoint(point);
                    var angle = Vector2.SignedAngle(bg.transform.right, localPoint);
                    range = ControlDegree2Range(angle);
                    if (Vector2.Distance(localPoint,handle.localPosition) < 1f)
                    {
                        SetRange(range);
                    }
                    
                }
                
                recordX = point.x;
            }

        }
        
        
    }

    void Init()
    {
        controlDegree = 0;
        radius = 2;
        recordX = -10;
        /*hitInfo = new RaycastHit2D[1];*/
        SetRange(0);
    }

    void SetRange(float range)
    {
        range = Mathf.Clamp01(range);
        controlDegree = Range2ControlDegree(range);//Mathf.Clamp(controlDegree, endDegree, startDegree);
        float x = radius*Mathf.Cos(controlDegree * Mathf.Deg2Rad);
        float y = radius*Mathf.Sin(controlDegree * Mathf.Deg2Rad);
        handle.localPosition = new Vector3(x, y, 0);
        range = ControlDegree2Range(controlDegree);
        //填充会随之变化
        fill.AngRadiansEnd = controlDegree * Mathf.Deg2Rad;
    }
    float ControlDegree2Range(float degree)
    {
        degree = Mathf.Clamp(degree, endDegree, startDegree);
        return 1 - (degree - endDegree) / (startDegree - endDegree);
    }

    float Range2ControlDegree(float range)
    {
        return (1 - range) * (startDegree - endDegree) + endDegree;
    }
    
    
}
