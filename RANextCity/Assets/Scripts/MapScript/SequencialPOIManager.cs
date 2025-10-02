using UnityEngine;

public class SequentialPOIManager : MonoBehaviour
{
    private POI[] pois;
    private int currentIndex = 0;

    void Start()
    {
        // Encontrar todos los POIs y ordenarlos por nombre (POI_01, POI_02...)
        pois = Object.FindObjectsByType<POI>(FindObjectsSortMode.None);
        System.Array.Sort(pois, (a, b) => a.name.CompareTo(b.name));

        // Al inicio, solo el primer POI está activado en escena
        for (int i = 0; i < pois.Length; i++)
        {
            pois[i].gameObject.SetActive(i == 0); // Activo solo POI_01
            pois[i].isActive = (i == 0);
        }

        Debug.Log($"🎯 POI inicial activo: {pois[0].poiName}");
    }

    public void OnPOIExit(POI poi)
    {
        // Desactivar completamente el objeto del POI actual
        poi.isActive = false;
        poi.gameObject.SetActive(false);

        // Activar el siguiente en la lista
        currentIndex++;
        if (currentIndex < pois.Length)
        {
            POI next = pois[currentIndex];
            next.isActive = true;
            next.gameObject.SetActive(true);

            Debug.Log($"➡️ Activado siguiente POI: {next.poiName}");
        }
        else
        {
            Debug.Log("✅ Ruta completada, no hay más POIs.");
        }
    }
}
