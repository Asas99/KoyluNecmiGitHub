using UnityEngine;
using UnityEngine.UI;

public class Arabaya�nBin : MonoBehaviour , IAraba
{
    public Text A��klamaText;
    public bool ArabadaM� { get; set; }
    public bool BinebilirMi { get; set; }
    public GameObject Araba;

    // Update is called once per frame
    void Update()
    {
        ArabayaBin�n();
    }

    public void ArabayaBin�n()
    {
        if (Input.GetKeyUp(KeyCode.T) && BinebilirMi)
        {
            ArabadaM� = !ArabadaM�;
        }

        if (BinebilirMi)
        {
            if (!ArabadaM�)
            {
                A��klamaText.text = "Arabaya binmek i�in '\x22' T '\x22' tu�una bas�n";
            }
            else
            {
                A��klamaText.text = "Arabadan inmek i�in '\x22' T '\x22' tu�una bas�n";
            }
        }

        //Parent'� g�ncelle. Parent oyuncu olarak ayarlan�yor ��nk� kamera onda.
        if (ArabadaM�)
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
