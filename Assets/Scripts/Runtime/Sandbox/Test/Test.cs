
using DG.Tweening;
using Shapes;
using UnityEngine;


namespace Runtime.Sandbox.CorePlay
{
    /// <summary>
    /// 测试用的
    /// </summary>
    public class Test : MonoBehaviour
    {
        public Polygon draw;

        public Disc dis;
        /*public Vector2[] vector2s;*/
        public Animator animator;
        [Range(-1,1)]
        public float time;

        public CryArc cryArc;

        private Tweener[] _tTweeners = new Tweener[3];
        
        // Start is called before the first frame update
        void Start()
        {
            /*draw.SetPointPosition(4,new Vector2(0.125f,-2));
            draw.SetPointPosition(5,new Vector2(-0.125f,-2));*/
            
            /*//四号点
            _tTweeners[0] = DOTween.To(() => draw.points[4], value => draw.SetPointPosition(4,value), new Vector2(0.125f, -5f), 1);
            //五号点
            _tTweeners[1] = DOTween.To(() => draw.points[5], value => draw.SetPointPosition(5,value), new Vector2(0.125f, -5f), 1);
            
            _tTweeners[1] = DOTween.To(() => draw.points[5], value => draw.SetPointPosition(5,value), new Vector2(0.125f, -5f), 1);
            for (int i = 0; i < _tTweeners.Length; i++)
            {
                _tTweeners[i].Pause();
                _tTweeners[i].SetAutoKill(false);
            }*/
            
        }

        // Update is called once per frame
        void Update()
        {
            
            
            /*if (Mathf.Approximately(cryArc.rangeDelta,0))
            {
                t.Pause();
            }
            else if (cryArc.rangeDelta > 0)
            {
                t.fullPosition = cryArc.range;
            }else if(cryArc.rangeDelta < 0)
            {
                t.fullPosition = cryArc.range;
                t.PlayBackwards();
            }*/
        }
    }
}
