using UnityEngine;
using System.Collections;

public class LocationPermission : MonoBehaviour
{
    // Variables públicas opcionales para acceder a lat/lon desde otros scripts
    public double latitude { get; private set; }
    public double longitude { get; private set; }

    void Start()
    {
#if UNITY_ANDROID
        // Pedir permiso de ubicación fina si no está concedido
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
        }
#endif
        // Iniciar la corrutina para activar el GPS
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
#if UNITY_ANDROID
        // Esperar a que el usuario conceda permiso
        while (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.FineLocation))
        {
            yield return null;
        }
#endif

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogWarning("GPS desactivado en el dispositivo");
            yield break;
        }

        // Iniciar el servicio de ubicación
        Input.location.Start();

        // Esperar hasta que el GPS se inicialice
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.LogError("Tiempo de espera GPS agotado");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("No se pudo obtener la ubicación");
            yield break;
        }

        Debug.Log("GPS activado correctamente");

        // Actualizar coordenadas iniciales
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        // Mantener actualizando la ubicación en tiempo real
        StartCoroutine(UpdateLocation());
    }

    private IEnumerator UpdateLocation()
    {
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;

                // Aquí puedes enviar las coordenadas a otro script, por ejemplo tu mapa
                // mapLoader.UpdatePosition(latitude, longitude);

                Debug.Log($"Ubicación actual: {latitude}, {longitude}");
            }
            yield return new WaitForSeconds(1f); // Actualiza cada segundo
        }
    }
}
