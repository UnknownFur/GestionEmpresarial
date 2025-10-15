using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class POIManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private LocationPermission locationPermission;
    [SerializeField] private MapLoader mapLoader;
    [SerializeField] private List<POI> poiList = new List<POI>();

    private int currentPOIIndex = 0;

    void Start()
    {
        if (poiList.Count == 0)
        {
            Debug.LogWarning("⚠️ No hay POIs asignados en el Inspector");
            return;
        }

        // Solo el primer POI está activo al inicio
        for (int i = 0; i < poiList.Count; i++)
        {
            poiList[i].gameObject.SetActive(i == 0);
            poiList[i].isActive = i == 0;
        }

        // Enviar los POIs visibles al mapa
        foreach (var poi in poiList)
        {
            string js = $"addPOI({poi.latitude}, {poi.longitude}, '{poi.poiName}')";
            mapLoader.SendJS(js);
        }

        Debug.Log($"🎯 POI inicial activo: {poiList[0].poiName}");
    }

    void Update()
    {
        if (locationPermission == null || poiList.Count == 0) return;

        double lat = locationPermission.latitude;
        double lon = locationPermission.longitude;
        mapLoader.UpdatePosition(lat, lon);

        POI activePOI = poiList[currentPOIIndex];
        float dist = Haversine(lat, lon, activePOI.latitude, activePOI.longitude);

        if (!activePOI.isActive && dist <= activePOI.activationRadius)
        {
            ActivatePOI(activePOI);
        }
        else if (activePOI.isActive && dist > activePOI.deactivationRadius)
        {
            DeactivatePOI(activePOI);
        }
    }

    void ActivatePOI(POI poi)
    {
        poi.isActive = true;
        mapLoader.ShowMap(false);

        var arCam = GameObject.Find("ARCamera");
        if (arCam != null)
        {
            var vuforiaBehaviour = arCam.GetComponent<VuforiaBehaviour>();
            if (vuforiaBehaviour != null)
                vuforiaBehaviour.enabled = true;
        }

        Debug.Log($"🚀 Activando POI: {poi.poiName}");
    }

    void DeactivatePOI(POI poi)
    {
        poi.isActive = false;
        mapLoader.ShowMap(true);

        var arCam = GameObject.Find("ARCamera");
        if (arCam != null)
        {
            var vuforiaBehaviour = arCam.GetComponent<VuforiaBehaviour>();
            if (vuforiaBehaviour != null)
                vuforiaBehaviour.enabled = false;
        }

        // Pasar al siguiente POI
        currentPOIIndex++;
        if (currentPOIIndex < poiList.Count)
        {
            POI next = poiList[currentPOIIndex];
            next.gameObject.SetActive(true);
            Debug.Log($"➡️ Activado siguiente POI: {next.poiName}");
        }
        else
        {
            Debug.Log("✅ Todos los POIs completados");
        }
    }

    float Haversine(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371000;
        double dLat = Mathf.Deg2Rad * (float)(lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (float)(lon2 - lon1);
        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                   Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2);
        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt(1 - (float)a));
        return (float)(R * c);
    }
}
