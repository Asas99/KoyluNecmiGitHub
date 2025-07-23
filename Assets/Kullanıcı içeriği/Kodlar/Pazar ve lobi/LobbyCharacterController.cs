using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    public TextMesh InfoText;
    public float speed = 5f;

    void Start()
    {
    }

    void Update()
    {
        InfoText.text = transform.position + "";
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }
}

