using UnityEngine;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        if (!isLocalPlayer) return;

        mainCamera = Camera.main;

        // Kameray� bu objeye kitler
        if (mainCamera != null)
        {
            mainCamera.transform.SetParent(null); // Kameray� parent'tan ay�r
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f); // Ba�lang�� pozisyonu
        }
    }

    void LateUpdate()
    {
        if (!isLocalPlayer || mainCamera == null) return;

        // Kamera oyuncuyu takip etsin
        Vector3 newPos = transform.position;
        newPos.z = -10f;
        mainCamera.transform.position = newPos;
    }
}
