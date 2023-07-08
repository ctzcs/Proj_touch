
using UnityEngine;
using UnityEngine.Events;

namespace SFrame
{
    /// <summary>
    /// Mono的管理者
    /// 1.声明周期函数
    /// 2.事件 
    /// 3.协程
    /// </summary>
    public class MonoController : MonoBehaviour {

        public event UnityAction UpdateEvent;
        public event UnityAction FixedUpdateEvent;

        // Use this for initialization
        void Start () {
            DontDestroyOnLoad(this.gameObject);
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        // Update is called once per frame
        void Update ()
        {
            UpdateEvent?.Invoke();
        }
    }
}
