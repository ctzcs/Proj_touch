using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Shapes;
using UnityEngine;

public class CryArc : MonoBehaviour
{
    public float radius;
    
    [Range(0,1)]
    public float range;
    public float lastRangeRecord;
    public float recordX;
    public float rangeDeltaSpeed;

    /// <summary>
    /// x正轴逆时针开始算的角度,结束的角度
    /// </summary>
    public float endDegree;
    /// <summary>
    /// 
    /// </summary>
    public float startDegree;

    public float controlDegree;
    /// <summary>
    /// 填充物
    /// </summary>
    public Disc fill;
    /// <summary>
    /// handle
    /// </summary>
    public Transform handle;
    
    [HideInInspector]
    public RaycastHit2D[] hitInfo;
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
            Physics2D.RaycastNonAlloc(point, Vector2.zero,hitInfo);
            Debug.Log(hitInfo[0]);
            if (hitInfo.Length > 0 )
            {
                if (hitInfo[0].collider.CompareTag("Handle"))
                {
                    var florence = point.x - recordX;
                    if (florence > 0.001f)
                    {
                        range += rangeDeltaSpeed * Time.deltaTime;
                        SetRange(range);
                        
                    }
                    recordX = point.x;
                }
            }
        }
        
        
    }

    void Init()
    {
        controlDegree = 0;
        radius = 2;
        recordX = -10;
        hitInfo = new RaycastHit2D[1];
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
        return 1 - (degree - endDegree) / (startDegree - endDegree);
    }

    float Range2ControlDegree(float range)
    {
        return (1 - range) * (startDegree - endDegree) + endDegree;
    }
    
    
}
