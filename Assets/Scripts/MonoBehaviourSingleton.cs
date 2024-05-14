using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; protected set; }


    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this as T;

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}