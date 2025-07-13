using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KomşuKöyüYükle : MonoBehaviour
{
    public Text IdText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayerPrefs.SetString("kullaniciId", IdText.text); // kullanıcı kendi ID'sini belirliyor
            SceneManager.LoadScene(1);
        }
    }
}
