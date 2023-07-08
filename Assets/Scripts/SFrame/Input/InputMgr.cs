using UnityEngine;

namespace SFrame
{
    /// <summary>
    /// 1.Input类
    /// 2.事件中心模块
    /// 3.公共Mono模块的使用
    /// </summary>
    public class InputMgr : BaseManager<InputMgr>
    {

        private bool _isStart = false;
        /// <summary>
        /// 构造函数中 添加Update监听
        /// </summary>
        public InputMgr()
        {
            MonoMgr.Instance.AddUpdateListener(MyUpdate);
        }

        /// <summary>
        /// 是否开启或关闭 我的输入检测
        /// </summary>
        public void StartOrEndCheck(bool isOpen)
        {
            _isStart = isOpen;
        }

        /// <summary>
        /// 用来检测按键抬起按下 分发事件的
        /// </summary>
        /// <param name="key"></param>
        private void CheckKeyCode(KeyCode key)
        {
            //事件中心模块 分发按下抬起事件
            if (Input.GetKeyDown(key))
                EventCenter.Instance.EventTrigger(EGlobalEvent.IsKeyPress, key);
            //事件中心模块 分发按下抬起事件
            if (Input.GetKeyUp(key))
                EventCenter.Instance.EventTrigger(EGlobalEvent.IsKeyRelease, key);
        }

        private void MyUpdate()
        {
            //没有开启输入检测 就不去检测 直接return
            if (!_isStart)
                return;

            CheckKeyCode(KeyCode.W);
            CheckKeyCode(KeyCode.S);
            CheckKeyCode(KeyCode.A);
            CheckKeyCode(KeyCode.D);
        }
	
    }
}
