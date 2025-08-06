using UnityEngine;
using UnityEngine.UI;

public class AraçUIManager : MonoBehaviour
{
    public Text VitesText;
    public ArabaKullan arabaKullan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindFirstObjectByType<ArabayaİnBin>().Araba != null)
        {
            arabaKullan = GameObject.FindAnyObjectByType<ArabayaİnBin>().Araba.GetComponent<ArabaKullan>();
        }

        if (arabaKullan != null)
        {
            VitesText.text = arabaKullan.sürüşTipi.ToString();
        }
        else
        {
            VitesText.text = "";
        }
    }
}
