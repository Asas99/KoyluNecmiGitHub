using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Otomatik sahne senkronizasyonu aktif � en kritik sat�r!
        PhotonNetwork.AutomaticallySyncScene = true;

        // T�m oyuncular ayn� versiyonda olmal�
        PhotonNetwork.GameVersion = "1.0";
    }

    public void ConnectToServer()
    {
        // Photon sunucusuna ba�lan
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Sunucuya bağlandı.");

        // Odaya gir veya yoksa olu�tur
        PhotonNetwork.JoinOrCreateRoom("KoyluRoom", new Photon.Realtime.RoomOptions
        {
            MaxPlayers = 10
        }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya girildi."); ;

        // Sadece Master Client sahneyi y�kler
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Client sahneyi y�kl�yor...");
            PhotonNetwork.LoadLevel("Pazar"); // D�KKAT: sahne ad�n� Build Settings'e g�re yaz!
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Odaya girilemedi: " + message);
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogWarning("Ba�lant� koptu: " + cause.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
