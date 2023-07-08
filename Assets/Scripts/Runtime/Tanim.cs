using System.Collections;
using UnityEngine;
using Shapes;
using UnityEngine.UI;
using UnityEditor;

public class Tanim : MonoBehaviour
{
    public Polygon m_TPolygon;

    public Polygon m_HPolygon;

    public Button m_Btn;

    public Animation OUC_Animation;

    private float animationSpeed = 1f;


    [Header("T上边界高度")]
    public float m_TUpHeight;
    [Header("T下边界高度")]
    public float m_TDownHeight;
    [Header("T左边界宽度")]
    public float m_TLeftWidth;
    [Header("T右边界宽度")]
    public float m_TRightWidth;

    [Header("H上边界高度")]
    public float m_HUpHeight;
    [Header("H下边界高度")]
    public float m_HDownHeight;
    [Header("H左边界宽度")]
    public float m_HLeftWidth;
    [Header("H右边界宽度")]
    public float m_HRightWidth;

    public float AnimationSpeed { get => animationSpeed; set => animationSpeed = value; }

    private void Start()
    {
        m_Btn.onClick.AddListener(()=>ReAnimation(-1 * AnimationSpeed));
        PlayAnimation(AnimationSpeed);
    }

    public void PlayAnimation(float speed)
    {
        StartCoroutine(TYAnimation(m_TDownHeight,speed));
        StartCoroutine(TXAnimation(m_TRightWidth, speed));
        StartCoroutine(HYAnimation(m_HUpHeight,speed));
    }

    public void ReAnimation(float speed)
    {
        PlayReverseAnimation(-speed);
        StartCoroutine(TYReAnimation(-1, speed));
        StartCoroutine(TXReAnimation(1, speed));
        StartCoroutine(HYReAnimation(1, speed));
    }

    void PlayReverseAnimation(float speed)
    {
        AnimationClip originalClip = OUC_Animation.GetClip("OUC"); 

        AnimationClip reverseClip = new AnimationClip();
        reverseClip.legacy = true; 

        foreach (var binding in AnimationUtility.GetCurveBindings(originalClip))
        {
            AnimationCurve originalCurve = AnimationUtility.GetEditorCurve(originalClip, binding);

            Keyframe[] originalKeyframes = originalCurve.keys;
            Keyframe[] reverseKeyframes = new Keyframe[originalKeyframes.Length];
            for (int i = 0; i < originalKeyframes.Length; i++)
            {
                reverseKeyframes[i] = new Keyframe(originalKeyframes[i].time, originalKeyframes[originalKeyframes.Length - 1 - i].value);
            }

            AnimationCurve reverseCurve = new AnimationCurve(reverseKeyframes);

            AnimationUtility.SetEditorCurve(reverseClip, binding, reverseCurve);
        }

        reverseClip.name = "Reversed_" + originalClip.name; 

        OUC_Animation.AddClip(reverseClip, reverseClip.name);
        OUC_Animation[reverseClip.name].speed = speed;
        OUC_Animation.Play(reverseClip.name); 
    }
    public IEnumerator TYAnimation(float limitLength, float speed)
    {

        while (m_TPolygon.points[4].y > limitLength)
        {
            m_TPolygon.SetPointPosition(4, 
                new Vector2(m_TPolygon.points[4].x, m_TPolygon.points[4].y - 0.001f * speed));
            m_TPolygon.SetPointPosition(5,
                new Vector2(m_TPolygon.points[5].x, m_TPolygon.points[5].y - 0.001f * speed));
            m_TPolygon.SetPointPosition(0,
                new Vector2(m_TPolygon.points[0].x, m_TPolygon.points[0].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(1,
                new Vector2(m_TPolygon.points[1].x, m_TPolygon.points[1].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(2,  
                new Vector2(m_TPolygon.points[2].x, m_TPolygon.points[2].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(3,
                new Vector2(m_TPolygon.points[3].x, m_TPolygon.points[3].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(6,
                new Vector2(m_TPolygon.points[6].x, m_TPolygon.points[6].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(7,
                new Vector2(m_TPolygon.points[7].x, m_TPolygon.points[7].y + 0.001f * speed));
            yield return null;
        }
    }

    
    public IEnumerator TYReAnimation(float limitLength, float speed)
    {

        while (m_TPolygon.points[4].y < limitLength)
        {
            m_TPolygon.SetPointPosition(4,
                new Vector2(m_TPolygon.points[4].x, m_TPolygon.points[4].y - 0.001f * speed));
            m_TPolygon.SetPointPosition(5,
                new Vector2(m_TPolygon.points[5].x, m_TPolygon.points[5].y - 0.001f * speed));
            m_TPolygon.SetPointPosition(0,
                new Vector2(m_TPolygon.points[0].x, m_TPolygon.points[0].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(1,
                new Vector2(m_TPolygon.points[1].x, m_TPolygon.points[1].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(2,
                new Vector2(m_TPolygon.points[2].x, m_TPolygon.points[2].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(3,
                new Vector2(m_TPolygon.points[3].x, m_TPolygon.points[3].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(6,
                new Vector2(m_TPolygon.points[6].x, m_TPolygon.points[6].y + 0.001f * speed));
            m_TPolygon.SetPointPosition(7,
                new Vector2(m_TPolygon.points[7].x, m_TPolygon.points[7].y + 0.001f * speed));
            yield return null;
        }
    }
    public IEnumerator TXAnimation(float limitLength, float speed)
    {
        while (m_TPolygon.points[1].x < limitLength)
        {
            m_TPolygon.SetPointPosition(1,
                new Vector2(m_TPolygon.points[1].x + 0.004f * speed, m_TPolygon.points[1].y));
            m_TPolygon.SetPointPosition(2,
                new Vector2(m_TPolygon.points[2].x + 0.004f * speed, m_TPolygon.points[2].y));

            yield return null;
        }
    }

    public IEnumerator TXReAnimation(float limitLength, float speed)
    {
        while (m_TPolygon.points[1].x > limitLength)
        {
            m_TPolygon.SetPointPosition(1,
                new Vector2(m_TPolygon.points[1].x + 0.004f * speed, m_TPolygon.points[1].y));
            m_TPolygon.SetPointPosition(2,
                new Vector2(m_TPolygon.points[2].x + 0.004f * speed, m_TPolygon.points[2].y));

            yield return null;
        }
    }
    public IEnumerator HYAnimation(float limitLength, float speed)
    {

        while (m_HPolygon.points[0].y < limitLength)
        {
            m_HPolygon.SetPointPosition(0,
                new Vector2(m_HPolygon.points[0].x, m_HPolygon.points[0].y + 0.001f * speed));
            m_HPolygon.SetPointPosition(1,
                new Vector2(m_HPolygon.points[1].x, m_HPolygon.points[1].y + 0.001f * speed));

            m_HPolygon.SetPointPosition(11,
                new Vector2(m_HPolygon.points[11].x, m_HPolygon.points[11].y - 0.001f * speed));
            m_HPolygon.SetPointPosition(10,
                new Vector2(m_HPolygon.points[10].x, m_HPolygon.points[10].y - 0.001f * speed));
            yield return null;
        }
    }
    public IEnumerator HYReAnimation(float limitLength, float speed)
    {
        while (m_HPolygon.points[0].y > limitLength)
        {
            m_HPolygon.SetPointPosition(0,
                new Vector2(m_HPolygon.points[0].x, m_HPolygon.points[0].y + 0.001f * speed));
            m_HPolygon.SetPointPosition(1,
                new Vector2(m_HPolygon.points[1].x, m_HPolygon.points[1].y + 0.001f * speed));

            m_HPolygon.SetPointPosition(11,
                new Vector2(m_HPolygon.points[11].x, m_HPolygon.points[11].y - 0.001f * speed));
            m_HPolygon.SetPointPosition(10,
                new Vector2(m_HPolygon.points[10].x, m_HPolygon.points[10].y - 0.001f * speed));
            yield return null;
        }
    }
}
