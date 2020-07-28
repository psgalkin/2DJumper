using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T c_instance;

    public static T Instance
    {
        get
        {
            if (c_instance == null)
            {
                c_instance = FindObjectOfType<T>();
                if (c_instance == null)
                {
                    Debug.LogError($"instance of type({typeof(T)}) do not exist");
                }
            }
            return c_instance;
        }
    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        T thisInstance = gameObject.GetComponent<T>();
        if (c_instance != null && c_instance != thisInstance)
        {
            Debug.LogError($"instance of type({typeof(T)}) alredy exist {gameObject.name} Destroyed (Duplicate)");
            Destroy(gameObject);
            return;
        }

        c_instance = thisInstance;
    }

    protected virtual void OnDestroy()
    {
        if (c_instance == this)
        {
            c_instance = null;
        }
    }
}
