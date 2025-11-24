using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class POIManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private LocationPermission locationPermission;
    [SerializeField] private MapLoader mapLoader;
    [SerializeField] private List<POI> poiList = new List<POI>();
    public RouteList routeList = null;
    private bool insidePOI = false;

    void Start()
    {
        StartCoroutine(InitializePOIs());
    }

    IEnumerator InitializePOIs()
    {
        yield return new WaitUntil(() =>
            routeList != null &&
            routeList.selectedRouteIndex != -1 &&
            routeList.poiList.Count > 0
        );

        poiList = routeList.poiList;

        RouteManager.Instance.SetTotalPOIs(poiList.Count);

        int index = Mathf.Clamp(RouteManager.Instance.currentPOIIndex, 0, poiList.Count - 1);

        for (int i = 0; i < poiList.Count; i++)
        {
            poiList[i].gameObject.SetActive(i == index);
            poiList[i].isActive = false;
        }

        Debug.Log($"🎯 POI activo: {poiList[index].poiName}");
    }

    void Update()
    {
        if (locationPermission == null || poiList.Count == 0) return;

        int index = RouteManager.Instance.currentPOIIndex;
        if (index >= poiList.Count) return;

        double lat = locationPermission.latitude;
        double lon = locationPermission.longitude;
        mapLoader.UpdatePosition(lat, lon);

        POI activePOI = poiList[index];
        float dist = Haversine(lat, lon, activePOI.latitude, activePOI.longitude);

        // 🔹 Entrando al POI
        if (!insidePOI && dist <= activePOI.activationRadius)
        {
            insidePOI = true;
            EnterPOI(activePOI);
        }
    }

    void EnterPOI(POI poi)
    {
        poi.isActive = true;
        Debug.Log($"🚀 Entrando al POI: {poi.poiName}");

        // Ocultar mapa y cargar escena AR
        mapLoader.ShowMap(false);
        SceneManager.LoadScene("AR"); // Nombre exacto de tu escena AR
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
