using UnityEngine;

public class GMTest : MonoBehaviour
{
    private static GMTest _instance;
    public static GMTest Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GMTest");
                go.AddComponent<GMTest>();
                _instance = go.GetComponent<GMTest>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
        set
        {
            if (_instance == null) _instance = value;
        }
    }

    public AudioManager audioManager;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (_instance != this) Destroy(this);
        }
    }

}
