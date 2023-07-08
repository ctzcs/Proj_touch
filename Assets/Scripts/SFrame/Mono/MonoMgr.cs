using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace SFrame
{
    /// <summary>
    /// 1.可以提供给外部添加帧更新事件的方法
    /// 2.可以提供给外部添加 协程的方法
    /// </summary>
    public class MonoMgr : BaseManager<MonoMgr>
    {
        private MonoController _controller;

        public MonoMgr()
        {
            //保证了MonoController对象的唯一性
            GameObject obj = new GameObject("MonoController");
            _controller = obj.AddComponent<MonoController>();
        }

        #region 帧更新
        /// <summary>
        /// 添加固定帧更新事件
        /// </summary>
        /// <param name="func"></param>
        public void AddFixedUpdateListener(UnityAction func)
        {
            _controller.FixedUpdateEvent += func;
        }
        
        /// <summary>
        /// 移除固定帧更新事件
        /// </summary>
        /// <param name="func"></param>
        public void RemoveFixedUpdateListener(UnityAction func)
        {
            _controller.FixedUpdateEvent -= func;
        }
        
        /// <summary>
        /// 添加帧更新事件
        /// </summary>
        /// <param name="func"></param>
        public void AddUpdateListener(UnityAction func)
        {
            _controller.UpdateEvent += func;
        }

        /// <summary>
        /// 用于移除帧更新事件
        /// </summary>
        /// <param name="func"></param>
        public void RemoveUpdateListener(UnityAction func)
        {
            _controller.UpdateEvent -= func;
        }
        

        #endregion

        #region 协程相关
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return _controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return _controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return _controller.StartCoroutine(methodName);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            _controller.StopCoroutine(routine);
        }

        public void StopAllCoroutine()
        {
            _controller.StopAllCoroutines();
        }

        #endregion
        
    }
}
