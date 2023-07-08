
using Shapes;
using UnityEngine;


public class CryArc : MonoBehaviour
{
    [Header("--描述Arc的参数--")]
    [Header("这样修改无效，必须在各个组件中改，他走的自己的流程")]
    public float radius;
    public float thickNess;
    /// <summary>
    /// x正轴逆时针开始算的角度,结束的角度
    /// </summary>
    public float endDegree;
    /// <summary>
    /// 开始的角度
    /// </summary>
    public float startDegree;
    
    [Header("鼠标控制的Range")]
    [Range(0,1)]
    public float range;
    /// <summary>
    /// 范围变化量
    /// </summary>
    public float rangeDelta;
    /// <summary>
    /// 如果范围状态不变，则记录为0
    /// </summary>
    public float recordRange;
    public float controlDegree;
    
    /// <summary>
    /// 填充物
    /// </summary>
    [Header("--Arc的组件--")]
    public Disc fill;
    /// <summary>
    /// 背景
    /// </summary>
    public Disc bg;

    /// <summary>
    /// handle
    /// </summary>
    public Disc handleDisc;
    public Transform handle;
    
    private float _recordX;

    public bool IsLevel;
    

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO range == 1进入关卡
        
        //用射线检测到range变化
        if (!IsLevel && Input.GetMouseButton(0))
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D info = Physics2D.Raycast(point, Vector2.zero, 0.1f);
            point.z = 0;
            if (info.collider.CompareTag("Handle"))
            {
                var florence = Mathf.Abs(point.x - _recordX);
                if (florence > 0.001f)
                {
                    var localPoint = transform.InverseTransformPoint(point);
                    var angle = Vector2.SignedAngle(bg.transform.right, localPoint);
                    float value = ControlDegree2Range(angle);
                    rangeDelta = value - range;
                    range = value;
                    if (Vector2.Distance(localPoint, handle.localPosition) < 1f)
                    {
                        SetRange(value);

                    }
                }

                _recordX = point.x;
            }
            if (range >= 1)
            {
                IsLevel = true;
                /*AnimDirector.GetInstance().SetAnimState(ECurrentState.EntryLevel);*/
            }
            recordRange = Mathf.Approximately(range, recordRange) ? 0 : range;
        }
        else recordRange = 0;


    }

    void Init()
    {
        controlDegree = 0;
        radius = 2f;
        _recordX = -10;
        //貌似他走的都是自己的流程，在别的地方改无效。
        thickNess = 0.25f;
        /*using (Draw.Command(Camera.main))
        {
            Draw.Radius = radius;
            Draw.Arc(this.transform.position,Quaternion.identity,startDegree * Mathf.Deg2Rad,endDegree * Mathf.Deg2Rad );
            bg.Radius = radius;
            bg.AngRadiansStart = startDegree * Mathf.Deg2Rad;
            bg.AngRadiansEnd = endDegree * Mathf.Deg2Rad;
        
        
            fill.Radius = radius;
        
            fill.AngRadiansStart = startDegree * Mathf.Deg2Rad;
        }*/
        
        
        
        SetRange(0);
    }
    /// <summary>
    /// 外界唯一需要这个接口的地方是动画倒放
    /// </summary>
    /// <param name="range"></param>
    public void SetRange(float range)
    {
        range = Mathf.Clamp01(range);
        controlDegree = Range2ControlDegree(range);//Mathf.Clamp(controlDegree, endDegree, startDegree);
        float x = radius * Mathf.Cos(controlDegree * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(controlDegree * Mathf.Deg2Rad);
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
