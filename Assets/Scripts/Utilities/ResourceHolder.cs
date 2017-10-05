using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    public static ResourceHolder Instance
    {
        get { return s_Instance; }
        private set { s_Instance = value; }
    }

    static ResourceHolder s_Instance;

    [SerializeField]
    protected PerSizeRestockSettings m_DefaultPerSizeRestockSettings;

    public static PerSizeRestockSettings DefaultPerSizeRestockSettings
    {
        get { return Instance.m_DefaultPerSizeRestockSettings; }
    }

    void Awake ()
    {
        if (Instance != null)
        {
            Destroy (gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
