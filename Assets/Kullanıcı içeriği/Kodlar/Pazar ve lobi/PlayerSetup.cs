using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView photonView;
    public Camera camera;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (!photonView.IsMine)
        {
            // Baþkasýnýn kamerasýysa kapat
            camera.enabled = false;
        }
        else
        {
            // Benim kameram, açýk býrak
            camera.enabled = true;
        }
    }
}
