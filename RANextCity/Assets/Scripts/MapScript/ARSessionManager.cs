using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARSessionManager : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Start()
    {
        int currentPOIIndex = RouteManager.Instance.currentPOIIndex;
        Debug.Log($"📍 AR iniciado para POI #{currentPOIIndex}");

        if (backButton != null)
            backButton.onClick.AddListener(OnBackToMap);
    }

    void OnBackToMap()
    {
        Debug.Log("🔙 Saliendo del modo AR... avanzando al siguiente POI");

        // Avanzar al siguiente POI
        RouteManager.Instance.NextPOI();

        // Volver al mapa
        SceneManager.LoadScene("Maps");
    }
}
