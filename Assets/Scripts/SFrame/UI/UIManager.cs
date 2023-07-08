using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SFrame
{
    /// <summary>
    /// UI层级
    /// </summary>
    public enum EUILayer
    {
        Bot,
        Mid,
        Top,
        System,
    }

    /// <summary>
    /// UI管理器
    /// 1.管理所有显示的面板
    /// 2.提供给外部 显示和隐藏等等接口
    /// </summary>
    public class UIManager : BaseManager<UIManager>
    {
        private Transform _bot;
        private Transform _mid;
        private Transform _top;
        private Transform _system;
        
        public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
        //记录我们UI的Canvas父对象 方便以后外部可能会使用它
        public RectTransform canvas;

        public UIManager()
        {
            //创建Canvas 让其过场景的时候 不被移除
            GameObject obj = ResMgr.Instance.Load<GameObject>("UI/Canvas");
            canvas = obj.transform as RectTransform;
            Object.DontDestroyOnLoad(obj);

            //找到各层
            if (canvas!=null)
            {
                _bot = canvas.Find("Bot");
                _mid = canvas.Find("Mid");
                _top = canvas.Find("Top");
                _system = canvas.Find("System"); 
            }
            
            //创建EventSystem 让其过场景的时候 不被移除
            obj = ResMgr.Instance.Load<GameObject>("UI/EventSystem");
            Object.DontDestroyOnLoad(obj);
        }

        /// <summary>
        /// 通过层级枚举 得到对应层级的父对象
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetLayerFather(EUILayer layer)
        {
            switch(layer)
            {
                case EUILayer.Bot:
                    return this._bot;
                case EUILayer.Mid:
                    return this._mid;
                case EUILayer.Top:
                    return this._top;
                case EUILayer.System:
                    return this._system;
            }
            return null;
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T">面板脚本类型</typeparam>
        /// <param name="panelName">面板名</param>
        /// <param name="layer">显示在哪一层</param>
        /// <param name="callBack">当面板预设体创建成功后 你想做的事</param>
        public void ShowPanel<T>(string panelName, EUILayer layer = EUILayer.Mid, UnityAction<T> callBack = null) where T:BasePanel
        {
            if (panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].ShowMe();
                // 处理面板创建完成后的逻辑
                if (callBack != null)
                    callBack(panelDic[panelName] as T);
                //避免面板重复加载 如果存在该面板 即直接显示 调用回调函数后  直接return 不再处理后面的异步加载逻辑
                return;
            }

            ResMgr.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
            {
                //把他作为 Canvas的子对象
                //并且 要设置它的相对位置
                //找到父对象 你到底显示在哪一层
                Transform father = _bot;
                switch(layer)
                {
                    case EUILayer.Mid:
                        father = _mid;
                        break;
                    case EUILayer.Top:
                        father = _top;
                        break;
                    case EUILayer.System:
                        father = _system;
                        break;
                }
                //设置父对象  设置相对位置和大小
                obj.transform.SetParent(father);

                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;

                ((RectTransform)obj.transform).offsetMax = Vector2.zero;
                ((RectTransform)obj.transform).offsetMin = Vector2.zero;

                //得到预设体身上的面板脚本
                T panel = obj.GetComponent<T>();
                // 处理面板创建完成后的逻辑
                if (callBack != null)
                    callBack(panel);

                panel.ShowMe();

                //把面板存起来
                panelDic.Add(panelName, panel);
            });
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string panelName)
        {
            if(panelDic.ContainsKey(panelName))
            {
                panelDic[panelName].HideMe();
                Object.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }

        /// <summary>
        /// 得到某一个已经显示的面板 方便外部使用
        /// </summary>
        public T GetPanel<T>(string name) where T:BasePanel
        {
            if (panelDic.TryGetValue(name, out var value))
                return value as T;
            return null;
        }

        /// <summary>
        /// 给控件添加自定义事件监听
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="type">事件类型</param>
        /// <param name="callBack">事件的响应函数</param>
        public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
        {
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = control.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);

            trigger.triggers.Add(entry);
        }

    }
}