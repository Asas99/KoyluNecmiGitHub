using UnityEngine;
using UnityEngine.UI;

public class CamFollow : MonoBehaviour
{
    public Camera cam;
    public Vector3 offset;
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = Player.transform.position + offset;
        cam.transform.LookAt(Player.transform);
    }
}
