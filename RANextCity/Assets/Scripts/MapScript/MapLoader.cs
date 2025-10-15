using UnityEngine;

public class MapLoader : MonoBehaviour
{
    private WebViewObject webViewObject;
    public bool mapReady = false;

    // Referencia al script que gestiona GPS en Unity
    [SerializeField] private LocationPermission locationPermission;

    void Start()
    {
        // Crear WebView
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

        // Cargar URL remota de tu mapa
        string url = "https://unknownfur.github.io/GestionEmpresarial/RANextCity/Assets/StreamingAssets/Map.html";
        webViewObject.LoadURL(url);
    }

    void Update()
    {
        // Enviar ubicación cada frame solo si el mapa ya está listo
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

    // 🔹 Enviar todos los POIs al mapa
    public void SendPOIs()
    {
        POI[] pois = Object.FindObjectsByType<POI>(FindObjectsSortMode.None);

        foreach (var poi in pois)
        {
            string js = $"addPOI({poi.latitude}, {poi.longitude}, '{poi.poiName}')";
            SendJS(js);
        }
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

    // 🔹 Recibir mensajes desde el mapa (JS → Unity)
    private void OnMessageFromJS(string msg)
    {
        Debug.Log("Mensaje desde JS: " + msg);

        if (msg == "map_ready")
        {
            OnMapReady();
        }
        else if (msg == "near_poi")
        {
            ShowMap(false);

            GameObject arCam = GameObject.Find("ARCamera");
            if (arCam != null)
                arCam.SetActive(true);

            Debug.Log("🚀 Activando AR porque llegaste al punto de interés");
        }
    }

    // 🔹 Cuando el mapa HTML avisa que está listo
    public void OnMapReady()
    {
        mapReady = true;
        Debug.Log("🟢 Mapa listo en WebView");

        // Enviar los POIs al mapa
        SendPOIs();

        // Enviar posición inicial del usuario (para el pin azul)
        if (locationPermission != null)
        {
            double lat = locationPermission.latitude;
            double lon = locationPermission.longitude;

            string js = $"updateUserPosition({lat}, {lon});";
            SendJS(js);
        }
    }

}
