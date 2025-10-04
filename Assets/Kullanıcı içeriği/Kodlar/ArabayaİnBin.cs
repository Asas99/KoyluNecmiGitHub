using UnityEngine;
using UnityEngine.UI;

public class Arabaya�nBin : MonoBehaviour , IAraba
{
    public Text A��klamaText;
    public bool ArabadaM� { get; set; }
    public bool BinebilirMi { get; set; }
    public GameObject Araba;

    [Header("Aray�z")]
    public GameObject ArabaUI;
    public Slider BenzinSlider;

    // Update is called once per frame
    void Update()
    {
        ArabayaBin�n();
    }

    public void ArabayaBin�n()
    {
        ArabaUI.SetActive(ArabadaM�);
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

        if (ArabadaM�)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.position = new Vector3(Araba.transform.position.x, Araba.transform.position.y, Araba.transform.position.z - 0.01f);
            BenzinSlider.maxValue = Araba.GetComponent<ArabaKullan>().MaxBenzin;
            BenzinSlider.value = Araba.GetComponent<ArabaKullan>().MevcutBenzin;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
        if (collision.CompareTag("Araba") && !ArabadaM�)
        {
            BinebilirMi = false;
            Araba = null;
        }
    }
}
