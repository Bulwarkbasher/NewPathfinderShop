using UnityEngine;

public class DefaultResourceHolder : MonoBehaviour
{
    public static DefaultResourceHolder Instance
    {
        get { return s_Instance; }
        private set { s_Instance = value; }
    }

    static DefaultResourceHolder s_Instance;

    [SerializeField]
    protected PerSizeRestockSettings m_DefaultPerSizeRestockSettings;
    [SerializeField]
    protected PerStockTypePerSizeAvailability m_DefaultPerStockTypePerSizeAvailability;
    [SerializeField]
    protected PerSizeRestockFrequencyModifiers m_DefaultPerSizeRestockFrequencyModifiers;

    public static PerSizeRestockSettings DefaultPerSizeRestockSettings
    {
        get { return Instance.m_DefaultPerSizeRestockSettings; }
    }

    public static PerStockTypePerSizeAvailability DefaultPerStockTypePerSizeAvailability
    {
        get { return Instance.m_DefaultPerStockTypePerSizeAvailability; }
    }

    public static PerSizeRestockFrequencyModifiers DefaultPerSizeRestockFrequencyModifiers
    {
        get { return Instance.m_DefaultPerSizeRestockFrequencyModifiers; }
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
