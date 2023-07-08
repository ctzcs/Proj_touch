

//1.C#中 泛型的知识
//2.设计模式中 单例模式的知识
namespace SFrame
{
    public class BaseManager<T> where T:new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
        }
    }
}

