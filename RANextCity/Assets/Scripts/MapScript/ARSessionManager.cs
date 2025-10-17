using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ARSessionManager : MonoBehaviour
{
    [SerializeField] private Button backButton;
    public TextMeshProUGUI descriptionText; // ← referencia al texto del Canvas


    void Start()
    {
        int currentPOIIndex = RouteManager.Instance.currentPOIIndex;
        Debug.Log($"📍 AR iniciado para POI #{currentPOIIndex}");

        if (backButton != null)
            backButton.onClick.AddListener(OnBackToMap);
        backButton.onClick.AddListener(OnBackToMap);

        // Buscar el POI actual y mostrar su descripción
        int index = RouteManager.Instance.currentPOIIndex;

        POI[] pois = FindObjectsOfType<POI>();
        if (index < pois.Length && descriptionText != null)
        {
            descriptionText.text = pois[index].description;
            Debug.Log($"Mostrando descripción del POI: {pois[index].poiName}");
        }
        else
        {
            Debug.LogWarning("No se pudo mostrar la descripción del POI (índice fuera de rango o sin referencia).");
        }

        
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
