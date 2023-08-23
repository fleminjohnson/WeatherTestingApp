using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherForeCasting
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            Debug.Log("parent awake method");
            if (instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Debug.LogWarning("Someone tring to create a duplicate of Singleton!");
                Destroy(this);
            }
        }
    }
}
