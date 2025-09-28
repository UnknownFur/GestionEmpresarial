using System.Collections;
using UnityEngine;

public class GPSController : MonoBehaviour
{
    private bool locationReady = false;
    private double latitude;
    private double longitude;

    IEnumerator Start()
    {
        // Verificar permisos
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("GPS no habilitado por el usuario");
            yield break;
        }

        // Iniciar servicio
        Input.location.Start(1f, 0.1f); // (precision en metros, distancia mínima en metros)

        // Esperar inicialización
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            Debug.LogWarning("Tiempo de espera de GPS agotado");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("No se pudo determinar la ubicación");
            yield break;
        }
        else
        {
            locationReady = true;
            Debug.Log("📍 GPS listo");
        }
    }

    void Update()
    {
        if (locationReady && Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
    }

    // 🔹 Saber si el GPS está listo
    public bool HasLocation()
    {
        return locationReady;
    }

    // 🔹 Obtener lat/lon
    public Vector2 GetLatLon()
    {
        return new Vector2((float)latitude, (float)longitude);
    }

    public double GetLatitude() => latitude;
    public double GetLongitude() => longitude;
}
