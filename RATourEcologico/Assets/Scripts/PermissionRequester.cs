using UnityEngine;
using UnityEngine.Android;

public class PermissionRequester : MonoBehaviour
{
    void Start()
    {
        // Pedir permiso de cámara
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

        // Pedir permiso de ubicación
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
    }
}
