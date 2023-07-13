using UnityEngine;

namespace Extensions
{
    /// <summary>
    /// Базовый класс-одиночка (Singleton) для компонентов MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">Тип компонента MonoBehaviour.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject("[SINGLETON]" + typeof(T));
                        _instance = singletonObject.AddComponent<T>();                    
                    }

                    if (_instance != null)
                    {
                        DontDestroyOnLoad(_instance);
                    }
                }
                
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }        
        }
    }
}