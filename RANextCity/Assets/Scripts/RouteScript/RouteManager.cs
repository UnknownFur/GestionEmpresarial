using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static RouteManager Instance { get; private set; }

    [Header("Estado de la Ruta")]
    public int currentPOIIndex = 0;
    public int totalPOIs = 0;

    void Awake()
    {
        // Singleton + persistencia entre escenas
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetTotalPOIs(int total)
    {
        totalPOIs = total;
    }

    public void NextPOI()
    {
        if (currentPOIIndex < totalPOIs - 1)
        {
            currentPOIIndex++;
            Debug.Log($"➡️ Pasando al siguiente POI: {currentPOIIndex}");
        }
        else
        {
            Debug.Log("✅ No hay más POIs en la ruta.");
        }
    }

    public void ResetRoute()
    {
        currentPOIIndex = 0;
        Debug.Log("🔄 Ruta reiniciada.");
    }
}
