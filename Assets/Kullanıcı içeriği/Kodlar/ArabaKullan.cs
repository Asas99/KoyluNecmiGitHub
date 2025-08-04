using UnityEngine;
using UnityEngine.UI;

public class ArabaKullan : MonoBehaviour , IAraba
{
    public Text UIText;
    public bool ArabadaMý { get; set; }
    public bool BinebilirMi { get; set; }
    public GameObject Araba;

    // Update is called once per frame
    void Update()
    {
        ArabayaBinÝn();
    }

    public void ArabayaBinÝn()
    {
        if (Input.GetKeyUp(KeyCode.T) && BinebilirMi)
        {
            ArabadaMý = !ArabadaMý;
        }

        if (BinebilirMi)
        {
            if (!ArabadaMý)
            {
                UIText.text = "Arabaya binmek için '\x22' T '\x22' tuþuna basýn";
            }
            else
            {
                UIText.text = "Arabadan inmek için '\x22' T '\x22' tuþuna basýn";
            }
        }

        //Parent'ý güncelle. Parent oyuncu olarak ayarlanýyor çünkü kamera onda.
        if (ArabadaMý)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent = Araba.transform;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if(Araba != null)
            {
                gameObject.transform.parent = null;
            }

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Araba"))
        {
            BinebilirMi = true;
            Araba = collision.transform.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Araba"))
        {
            BinebilirMi = false;
            Araba = null;
        }
    }
}
