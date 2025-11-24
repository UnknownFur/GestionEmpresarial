using System.Collections.Generic;
using UnityEngine;

public class RouteList : MonoBehaviour
{
    public List<GameObject> routes = new List<GameObject>();
    public List<POI> poiList = new List<POI>();
    public int selectedRouteIndex;
    public UserData userData;

    private void OnEnable() {
        userData = FindFirstObjectByType<UserData>();
        LoadRoute();
    }

    public void LoadRoute()
    {
        poiList.Clear();
        selectedRouteIndex = userData.selectedRoute;
        ActiveRoute();
        GetPOIsFromRoute();
    }

    void ActiveRoute()
    {
        for (int i = 0; i < routes.Count; i++)
        {
            routes[i].SetActive(i == selectedRouteIndex);
        }
    }

    void GetPOIsFromRoute()
    {
        Transform routeTransform = routes[selectedRouteIndex].transform;
        foreach (Transform child in routeTransform)
        {
            POI poi = child.GetComponent<POI>();
            if (poi != null)
            {
                poiList.Add(poi);
            }
        }
    }
}