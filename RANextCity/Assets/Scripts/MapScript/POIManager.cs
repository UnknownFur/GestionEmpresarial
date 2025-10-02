using UnityEngine;
using Vuforia;
public class POIManager : MonoBehaviour
{
    [SerializeField] private LocationPermission locationPermission; // Asignar en Inspector
    [SerializeField] private MapLoader mapLoader;
    private POI[] pois;

    void Start()
    {
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
        if (locationPermission == null) return;

        double lat = locationPermission.latitude;
        double lon = locationPermission.longitude;

        mapLoader.UpdatePosition(lat, lon);

        foreach (var poi in pois)
        {
            float dist = Haversine(lat, lon, poi.latitude, poi.longitude);

            if (!poi.isActive && dist <= poi.activationRadius)
            {
                poi.isActive = true;
                Debug.Log($"🚀 Activando POI: {poi.poiName}");
                ActivatePOI(poi);
            }

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
        mapLoader.ShowMap(false);

        var arCam = GameObject.Find("ARCamera");
        if (arCam != null)
        {
            var vuforiaBehaviour = arCam.GetComponent<VuforiaBehaviour>();
            if (vuforiaBehaviour != null)
            {
                vuforiaBehaviour.enabled = true; // activa tracking
            }
        }

        Debug.Log($"🚀 Activando AR: {poi.poiName}");
    }

    void DeactivatePOI(POI poi)
    {
        mapLoader.ShowMap(true);

        var arCam = GameObject.Find("ARCamera");
        if (arCam != null)
        {
            var vuforiaBehaviour = arCam.GetComponent<VuforiaBehaviour>();
            if (vuforiaBehaviour != null)
            {
                vuforiaBehaviour.enabled = false; // pausa tracking
            }
        }

        Debug.Log($"📍 Saliste del POI: {poi.poiName}, tracking pausado");
        var seqManager = FindAnyObjectByType<SequentialPOIManager>();
        if (seqManager != null)
        {
            seqManager.OnPOIExit(poi);
        }
    }

    float Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371000; // metros
        double dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);
        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                   Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2);
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
        return (float)(R * c);
    }
}
