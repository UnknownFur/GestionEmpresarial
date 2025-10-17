using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ARSessionManager : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnBackToMap);
        backButton.onClick.AddListener(OnBackToMap);
    }

    void OnBackToMap()
    {
        // Avanzar al siguiente POI
        RouteManager.Instance.NextPOI();

        // Volver al mapa
        SceneManager.LoadScene("Maps");
    }
}
