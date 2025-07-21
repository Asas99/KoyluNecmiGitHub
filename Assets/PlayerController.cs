using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public TextMesh nameText;
    public float moveSpeed = 5f;

    void Start()
    {
        if (isLocalPlayer)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Anonim");
            CmdSetName(playerName);
        }
    }

    [Command]
    void CmdSetName(string name)
    {
        RpcSetName(name);
    }

    [ClientRpc]
    void RpcSetName(string name)
    {
        nameText.text = name;
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, v, 0) * moveSpeed * Time.deltaTime);
    }
}
