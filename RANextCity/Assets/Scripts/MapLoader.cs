using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private WebViewObject webViewObject;
    public bool mapReady = false;

    // Referencia al script de ubicación
    [SerializeField] private LocationPermission locationPermission;

    void Start()
    {
        // Crear el WebView
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init(
            cb: (msg) => { OnMessageFromJS(msg); },
            err: (msg) => { Debug.LogError("WebView Error: " + msg); },
            started: (msg) => { Debug.Log("WebView started: " + msg); },
            hooked: (msg) => { Debug.Log("WebView hooked: " + msg); },
            ld: (msg) => { Debug.Log("WebView loaded: " + msg); }
        );

        webViewObject.SetMargins(0, 0, 0, 0);
        webViewObject.SetVisibility(true);

        // Cargar URL remota
        string url = "https://unknownfur.github.io/GestionEmpresarial/RANextCity/Assets/StreamingAssets/Map.html";
        webViewObject.LoadURL(url);
    }

    void Update()
    {
        // Cada frame enviamos la ubicación al WebView
        if (locationPermission != null && mapReady)
        {
            UpdatePosition(locationPermission.latitude, locationPermission.longitude);
        }
    }

    // 🔹 Actualizar la posición del usuario en el mapa (Unity → JS)
    public void UpdatePosition(double lat, double lon)
    {
        string js = $"updateUserPosition({lat}, {lon})";
        SendJS(js);
    }

    // 🔹 Ejecutar cualquier JS desde Unity
    public void SendJS(string jsCode)
    {
        if (webViewObject != null)
            webViewObject.EvaluateJS(jsCode);
    }

    // 🔹 Mostrar / ocultar el mapa
    public void ShowMap(bool state)
    {
        if (webViewObject != null)
            webViewObject.SetVisibility(state);
    }

    // 🔹 Recibir mensajes del mapa (JS → Unity)
    private void OnMessageFromJS(string msg)
    {
        Debug.Log("Mensaje desde JS: " + msg);

        if (msg == "near_poi")
        {
            ShowMap(false);

            GameObject arCam = GameObject.Find("ARCamera");
            if (arCam != null)
                arCam.SetActive(true);

            Debug.Log("🚀 Activando AR porque llegaste al punto de interés");
        }
    }
    public void OnMapReady()
    {
        mapReady = true;
        Debug.Log("🟢 Mapa listo en WebView");
    }
}
