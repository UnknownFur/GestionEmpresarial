using UnityEngine;

public class POIManager : MonoBehaviour
{
    private GPSController gps;
    private MapLoader mapLoader;
    private POI[] pois;

    void Start()
    {
        gps = GetComponent<GPSController>();
        mapLoader = GetComponent<MapLoader>();
        pois = Object.FindObjectsByType<POI>(FindObjectsSortMode.None);

        // Enviar los POI al mapa (JS)
        foreach (var poi in pois)
        {
            string js = string.Format("addPOI({0}, {1}, '{2}')", 
                                       poi.latitude, poi.longitude, poi.poiName);
            mapLoader.SendJS(js);
        }
    }

    void Update()
    {
        if (gps == null || !gps.HasLocation()) return;

        Vector2 userPos = gps.GetLatLon();
        mapLoader.UpdatePosition(userPos.x, userPos.y);

        foreach (var poi in pois)
        {
            float dist = Haversine(userPos.x, userPos.y, poi.latitude, poi.longitude);

            // 🔹 Entrar en el POI → activar RA
            if (!poi.isActive && dist <= poi.activationRadius)
            {
                poi.isActive = true;
                Debug.Log($"🚀 Activando POI: {poi.poiName}");
                ActivatePOI(poi);
            }

            // 🔹 Salir del POI → desactivar RA y volver al mapa
            if (poi.isActive && dist > poi.deactivationRadius)
            {
                poi.isActive = false;
                Debug.Log($"⬅️ Saliendo del POI: {poi.poiName}");
                DeactivatePOI(poi);
            }
        }
    }


    void ActivatePOI(POI poi)
    {
        // Ocultar mapa
        mapLoader.ShowMap(false);

        // Activar AR
        GameObject arCam = GameObject.Find("ARCamera");
        if (arCam != null)
            arCam.SetActive(true);

        // Aquí también puedes activar un prefab de información en RA
    }

    void DeactivatePOI(POI poi)
    {
        // Mostrar mapa otra vez
        mapLoader.ShowMap(true);

        // Apagar la ARCamera
        GameObject arCam = GameObject.Find("ARCamera");
        if (arCam != null)
            arCam.SetActive(false);

        Debug.Log($"📍 Volviendo al mapa, saliste del POI: {poi.poiName}");
    }

    float Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371000; // Radio de la Tierra en metros
        double dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);
        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                   Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2);
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
        return (float)(R * c);
    }
}
