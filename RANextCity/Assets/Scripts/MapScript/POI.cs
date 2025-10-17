using UnityEngine;

public class POI : MonoBehaviour
{
    [Header("Coordenadas GPS")]
    public double latitude;
    public double longitude;

    [Header("Configuración")]
    public string poiName;

    [Tooltip("Distancia para activar la RA (m)")]
    public float activationRadius = 20f;

    [Tooltip("Distancia para desactivar la RA (m)")]
    public float deactivationRadius = 30f;

    [HideInInspector] public bool isActive = false;

    public string description; // Texto explicativo del lugar

}
