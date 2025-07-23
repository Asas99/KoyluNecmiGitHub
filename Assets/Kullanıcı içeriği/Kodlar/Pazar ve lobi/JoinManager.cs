using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class JoinManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom çağrıldı. Oyuncu: " + PhotonNetwork.NickName);
        SpawnPlayer(); // Sahne y�klendiyse burada da �a��r
    }

    void SpawnPlayer()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-5, 5), Random.Range(-3, 3), 0);
            var obj = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
            Debug.Log("Oyuncu doğdu: " + obj.name);
        }
        else
        {
            Debug.LogWarning("Photon hazır değil, doğurulamadı!");
        }
    }
}
