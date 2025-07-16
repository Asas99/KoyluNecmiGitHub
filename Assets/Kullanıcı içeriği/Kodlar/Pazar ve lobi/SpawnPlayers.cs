using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public Vector2 MinVecs , MaxVecs;
    public GameObject PlayerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 RandomPos = new Vector2(Random.Range(MinVecs.x,MaxVecs.x),Random.Range(MinVecs.y,MaxVecs.y));
        PhotonNetwork.Instantiate(PlayerPrefab.name, RandomPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
