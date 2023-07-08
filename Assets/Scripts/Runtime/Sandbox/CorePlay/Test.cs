using System.Collections.Generic;
using Shapes;
using UnityEngine;
using UnityEngine.Playables;

namespace Runtime.Sandbox.CorePlay
{
    public class Test : MonoBehaviour
    {
        /*public Polygon draw;*/
        /*public Vector2[] vector2s;*/
        public Animator animator;
        [Range(-1,1)]
        public float time;
        // Start is called before the first frame update
        void Start()
        {
            /*draw.SetPointPosition(4,new Vector2(0.125f,-2));
            draw.SetPointPosition(5,new Vector2(-0.125f,-2));*/
        }

        // Update is called once per frame
        void Update()
        {
            animator.Play("TestTriangle");
            animator.speed = 1;
            var info = animator.GetCurrentAnimatorStateInfo(0);
            
            animator.SetFloat("speedMultiply",time);
            /*draw.points = points;*/
        }
    }
}
