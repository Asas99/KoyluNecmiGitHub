using UnityEngine;
using UnityEngine.UI;

public class KarakterUIManager : MonoBehaviour
{
    public Slider CanSlider;
    public Text ParaText;

    public KarakterHareket karakterHareket;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        karakterHareket = GameObject.FindFirstObjectByType<KarakterHareket>(); 
        CanSlider.maxValue = karakterHareket.Can;
    }

    // Update is called once per frame
    void Update()
    {
        ParaText.text = "Para: " + karakterHareket.Para + "₺";
        CanSlider.value = karakterHareket.Can;
    }
}
