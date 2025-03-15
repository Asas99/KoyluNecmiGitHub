using UnityEngine;
using UnityEngine.UI;

public class KarakterHareket : OrtakÖzellikler, IHareket
{
    public int Para;
    public int Can;

    public void Yürü()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(x, y, 0) * YürümeHýzý * Time.deltaTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Yürü();
    }
}
