using System.Collections.Generic;
using UnityEngine.Events;

namespace SFrame
{
    /// <summary>
    /// 时间信息接口
    /// </summary>
    public interface IEventInfo { }

    /// <summary>
    /// 事件信息类(泛型)
    /// </summary>
    /// <typeparam name="T">传递参数类型</typeparam>
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo( UnityAction<T> action)
        {
            actions += action;
        }
    }

    /// <summary>
    /// 事件信息类
    /// </summary>
    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    /// <summary>
    /// 事件中心
    /// </summary>
    public class EventCenter : BaseManager<EventCenter>
    {
        //key —— 事件的名字（比如：怪物死亡，玩家死亡，通关 等等）
        //value —— 对应的是 监听这个事件 对应的委托函数们
        private Dictionary<EGlobalEvent, IEventInfo> _eventDic = new Dictionary<EGlobalEvent, IEventInfo>();

        /// <summary>
        /// 添加事件监听(有参)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">处理事件的委托</param>
        public void AddEventListener<T>(EGlobalEvent name, UnityAction<T> action)
        {
            //有没有对应的事件监听
            //有的情况
            if( _eventDic.TryGetValue(name, out IEventInfo value) )
            {
                ((EventInfo<T>)value).actions += action;
            }
            //没有的情况
            else
            {
                _eventDic.Add(name, new EventInfo<T>( action ));
            }
        }

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="action">处理事件的委托</param>
        public void AddEventListener(EGlobalEvent name, UnityAction action)
        {
            //有没有对应的事件监听
            //有的情况
            if (_eventDic.TryGetValue(name, out var value))
            {
                ((EventInfo)value).actions += action;
            }
            //没有的情况
            else
            {
                _eventDic.Add(name, new EventInfo(action));
            }
        }


        /// <summary>
        /// 移除事件监听(有参)
        /// </summary>
        /// <param name="name">事件的名字</param>
        /// <param name="action">对应之前添加的委托函数</param>
        public void RemoveEventListener<T>(EGlobalEvent name, UnityAction<T> action)
        {
            if (_eventDic.TryGetValue(name, out var value))
                ((EventInfo<T>)value).actions -= action;
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="name">事件的名称</param>
        /// <param name="action">对应之前添加的委托函数</param>
        public void RemoveEventListener(EGlobalEvent name, UnityAction action)
        {
            if (_eventDic.TryGetValue(name, out var value))
                ((EventInfo)value).actions -= action;
        }

        /// <summary>
        /// 事件触发器(有参)
        /// </summary>
        /// <param name="name">事件名</param>
        /// <param name="info">触发时传递的信息</param>
        public void EventTrigger<T>(EGlobalEvent name, T info)
        {
            //有没有对应的事件监听
            //有的情况
            if (_eventDic.TryGetValue(name,out IEventInfo value))
            {
                ((EventInfo<T>)value).actions?.Invoke(info);
            }
        }

        /// <summary>
        /// 事件触发器
        /// </summary>
        /// <param name="name">事件名</param>
        public void EventTrigger(EGlobalEvent name)
        {
            //有没有对应的事件监听
            //有的情况
            if (_eventDic.TryGetValue(name,out IEventInfo value))
            {
                ((EventInfo)value).actions?.Invoke();
            }
        }

        /// <summary>
        /// 清空事件中心
        /// 主要用在 场景切换时
        /// </summary>
        public void Clear()
        {
            _eventDic.Clear();
        }
    }
}