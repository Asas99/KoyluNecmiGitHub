using UnityEngine;
using UnityEngine.UI;

public class ArabayaÝnBin : MonoBehaviour , IAraba
{
    public Text AçýklamaText;
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
                AçýklamaText.text = "Arabaya binmek için '\x22' T '\x22' tuþuna basýn";
            }
            else
            {
                AçýklamaText.text = "Arabadan inmek için '\x22' T '\x22' tuþuna basýn";
            }
        }

        if (ArabadaMý)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.position = new Vector3(Araba.transform.position.x, Araba.transform.position.y, Araba.transform.position.z - 0.01f);
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
        if (collision.CompareTag("Araba") && !ArabadaMý)
        {
            BinebilirMi = false;
            Araba = null;
        }
    }
}
