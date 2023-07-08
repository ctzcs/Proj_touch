using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SFrame
{
    public class MusicMgr : BaseManager<MusicMgr>
    {
        //唯一的背景音乐组件
        private AudioSource _bkMusic;
        //音乐大小
        private float _bkValue = 1;

        //音效依附对象,这里如果用一个对象放音效，那么就不能让音效出现在不同的地方，3D的话肯定不能这样
        //private GameObject _soundObj;
        
        //音效列表
        private List<AudioSource> _soundList = new List<AudioSource>();
        //音效大小
        private float _soundValue = 1;

        public MusicMgr()
        {
            MonoMgr.Instance.AddUpdateListener(Update);
        }

        private void Update()
        {
            for( int i = _soundList.Count - 1; i >=0; --i )
            {
                if(!_soundList[i].isPlaying)
                {
                    //GameObject.Destroy(soundList[i]);
                    //修改成从对象池拿取
                    PoolMgr.Instance.PushObj(_soundList[i].gameObject.name,_soundList[i].gameObject);
                    _soundList.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name"></param>
        public void PlayBkMusic(string name)
        {
            if(_bkMusic == null)
            {
                GameObject obj = new GameObject();
                obj.name = "BkMusic";
                _bkMusic = obj.AddComponent<AudioSource>();
            }
            //异步加载背景音乐 加载完成后 播放
            ResMgr.Instance.LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
            {
                _bkMusic.clip = clip;
                _bkMusic.loop = true;
                _bkMusic.volume = _bkValue;
                _bkMusic.Play();
            });

        }

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public void PauseBkMusic()
        {
            if (_bkMusic == null)
                return;
            _bkMusic.Pause();
        }

        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBkMusic()
        {
            if (_bkMusic == null)
                return;
            _bkMusic.Stop();
        }

        /// <summary>
        /// 改变背景音乐 音量大小
        /// </summary>
        /// <param name="v"></param>
        public void ChangeBkValue(float v)
        {
            _bkValue = v;
            if (_bkMusic == null)
                return;
            _bkMusic.volume = _bkValue;
        }

        /// <summary>
        /// 播放音效,音效Obj统一命名为SFX
        /// </summary>
        public void PlaySound(string name, bool isLoop, UnityAction<AudioSource> callBack = null)
        {
            /*if(_soundObj == null)
            {
                _soundObj = new GameObject();
                _soundObj.name = "SFX";
            }*/
            //当音效资源异步加载结束后 再添加一个音效
            AudioClip clip = ResMgr.Instance.Load<AudioClip>("Music/Sound/" + name);
            PoolMgr.Instance.GetObj("SFX", (obj) =>
            {
                if (!obj.TryGetComponent(out AudioSource source))
                {
                    source = obj.AddComponent<AudioSource>();
                }
                source.clip = clip;
                source.loop = isLoop;
                source.volume = _soundValue;
                source.Play();
                _soundList.Add(source);
                callBack?.Invoke(source);
            });
        }

        /// <summary>
        /// 改变音效声音大小
        /// </summary>
        /// <param name="value"></param>
        public void ChangeSoundValue( float value )
        {
            _soundValue = value;
            for (int i = 0; i < _soundList.Count; ++i)
                _soundList[i].volume = value;
        }

        /// <summary>
        /// 停止音效
        /// </summary>
        public void StopSound(AudioSource source)
        {
            if( _soundList.Contains(source) )
            {
                _soundList.Remove(source);
                source.Stop();
                PoolMgr.Instance.PushObj(source.name,source.gameObject);
            }
        }
    }
}
