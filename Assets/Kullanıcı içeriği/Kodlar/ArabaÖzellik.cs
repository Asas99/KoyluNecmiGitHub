using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ArabaÖzellik : MonoBehaviour
{
    [Range(1, 1000)]
    public float MaxSürüşHızı, MaxDönüşHızı;
    public float SürüşHızı, DönüşHızı;
    public float Çizgiselİvme, Açısalİvme;
    public float ÇizgiselSürtünme, AçısalSürtünme;
    public bool KullanılıyorMu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SürüşHızı = Mathf.Clamp(SürüşHızı, -MaxSürüşHızı, MaxSürüşHızı);
        DönüşHızı = Mathf.Clamp(DönüşHızı, -MaxDönüşHızı, MaxDönüşHızı);

        if (GameObject.FindFirstObjectByType<ArabaKullan>().Araba == gameObject)
        {
            if (GameObject.FindFirstObjectByType<ArabaKullan>().ArabadaMı == true)
            {
                KullanılıyorMu = true;
            }
        }

        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (KullanılıyorMu)
        {
            //Girdi yoksa yavaşlat
            if (x == 0)
            {
                DönüşHızı = Mathf.MoveTowards(DönüşHızı, 0, AçısalSürtünme * Time.deltaTime);
            }
            else if (x != 0)
            {
                DönüşHızı += Açısalİvme * Time.deltaTime* -x;
            }
            if (y == 0)
            {
                SürüşHızı = Mathf.MoveTowards(SürüşHızı, 0, ÇizgiselSürtünme * Time.deltaTime);
            }
            else if (y != 0)
            {
                SürüşHızı += Çizgiselİvme * Time.deltaTime * y;
            }

            transform.Translate(transform.up * Time.deltaTime * SürüşHızı, Space.World);
            transform.Rotate(Vector3.forward * DönüşHızı * Time.deltaTime);
        }
    }

}
