using UnityEngine;

/*
Singleton base class.
For any given singleton, only one can exist throughout the game's lifetime.
This should be used for high-order game objects (GameManager, AudioManager, etc.)

This should not be used for entities (Player, Boss, NPC, items).
*/

public abstract class BunnySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if(_instance == null)
                {
                    var singletonObj = new GameObject();
                    singletonObj.name = typeof(T).ToString();
                    _instance = singletonObj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = GetComponent<T>();

        DontDestroyOnLoad(gameObject);

        if(_instance == null)
            return;
    }
}