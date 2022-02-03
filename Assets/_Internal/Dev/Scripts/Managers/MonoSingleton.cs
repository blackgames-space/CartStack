using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null) UnityEngine.Debug.Log("There's no " + typeof(T).ToString());
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            UnityEngine.Debug.LogError("There's more than one " + typeof(T).ToString());
            return;
        }

        _instance = this as T;    
    }
}
