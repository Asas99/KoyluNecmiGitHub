using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }
    public void JoinRoom()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 10,    // Maksimum oyuncu sayýsý
            IsOpen = true,
            IsVisible = true
        };

        PhotonNetwork.JoinOrCreateRoom("Pazar", options, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Pazar");
    }
}
