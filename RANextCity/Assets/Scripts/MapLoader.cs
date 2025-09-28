using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private UnityEngine.Object htmlFile;
    private WebViewObject webViewObject;

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

        // Cargar el HTML desde StreamingAssets
        if(htmlFile == null)
        {
            Debug.LogError("No has asignado ningún archivo HTML");
            return;
        }

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, htmlFile.name + ".html");

        #if UNITY_ANDROID
            webViewObject.LoadURL(path.Replace("file://", "jar:file://"));
        #else
            webViewObject.LoadURL("file://" + path);
        #endif
    }
    // 🔹 Actualizar la posición del usuario en el mapa (Unity → JS)
    public void UpdatePosition(double lat, double lon)
    {
        string js = $"updateUserPosition({lat}, {lon})";
        SendJS(js);
    }

    // 🔹 Nuevo → Ejecutar cualquier JS desde Unity
    public void SendJS(string jsCode)
    {
        if (webViewObject != null)
            webViewObject.EvaluateJS(jsCode);
    }

    // 🔹 Nuevo → Mostrar / ocultar el mapa
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
            // Ocultar el mapa (WebView)
            ShowMap(false);

            // Activar la ARCamera de Vuforia
            GameObject arCam = GameObject.Find("ARCamera");
            if (arCam != null)
                arCam.SetActive(true);

            Debug.Log("🚀 Activando AR porque llegaste al punto de interés");
        }
    }
}
