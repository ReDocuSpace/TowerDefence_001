using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    var obj = new GameObject(typeof(T).ToString());
                    _instance = obj.AddComponent<T>();  // ¿Ã ∂ß Awake() »£√‚µ 

                    Debug.LogFormat("Create singleton : {0}", _instance.name);
                    _instance.Init();

                    //Problem during the creation, this should not happen
                    if (_instance == null)
                    {
                        Debug.LogFormat("Not Creation singleton : {0}", _instance.name);
                    }
                }
                else
                {

                    Debug.LogFormat("Find singleton : {0}", _instance.name);
                    _instance.Init();
                }
            }
            return _instance;
        }
    }

    public abstract void Init();
    public abstract void Release();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instance = this as T;
            _instance.Init();
            Debug.LogFormat("Awake Singleton : {0}", _instance.name);
        }
        else if (_instance != this)
        {
            Debug.LogFormat("Refresh Singleton : {0}", _instance.name);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            Debug.LogFormat("Destory singleton : {0}", _instance.name);

            _instance.Release();

            _instance = null;
        }

        Destroy(gameObject);
    }

}

