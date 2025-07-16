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
            // Ba�kas�n�n kameras�ysa kapat
            camera.enabled = false;
        }
        else
        {
            // Benim kameram, a��k b�rak
            camera.enabled = true;
        }
    }
}
