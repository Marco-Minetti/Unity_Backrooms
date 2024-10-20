using UnityEngine;

public class LunghezzaManager : MonoBehaviour
{
    public static LunghezzaManager Instance { get; private set; }
    public int lunghezza; // The variable to change

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}
