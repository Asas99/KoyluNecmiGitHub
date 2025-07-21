using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public InputField playerNameInput;
    public Button newVillageButton;
    public Button continueButton;
    public Button enterCityButton;
    public Button cityServerButton; // Sadece þehir sunucusu için (build'de görünür)

    const string PlayerNameKey = "PlayerName";
    const string HasVillageKey = "HasVillage";

    void Start()
    {
        // Oyuncu ismi varsa otomatik doldur
        if (PlayerPrefs.HasKey(PlayerNameKey))
            playerNameInput.text = PlayerPrefs.GetString(PlayerNameKey);

        // Daha önce köy oluþturulmuþsa Devam Et aktif olur
        continueButton.interactable = PlayerPrefs.HasKey(HasVillageKey);

        // Þehir server butonunu sadece server build'de göster
#if UNITY_EDITOR
        cityServerButton.gameObject.SetActive(false);
#else
        cityServerButton.gameObject.SetActive(IsCityServerBuild());
#endif
    }

    bool IsCityServerBuild()
    {
        // Örneðin PlayerPrefs veya komut satýrý argümanýna göre kontrol edebilirsin
        return true; // Þimdilik hep true, istersen özelleþtirebiliriz
    }

    public void OnClick_NewVillage()
    {
        string playerName = playerNameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Oyuncu ismi boþ olamaz.");
            return;
        }

        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.SetInt(HasVillageKey, 1);
        PlayerPrefs.Save();

        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.ServerChangeScene("VillageScene");
    }

    public void OnClick_Continue()
    {
        if (!PlayerPrefs.HasKey(HasVillageKey))
        {
            Debug.LogWarning("Daha önce köy oluþturulmamýþ.");
            return;
        }

        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.ServerChangeScene("VillageScene");
    }

    public void OnClick_EnterCity()
    {
        string playerName = playerNameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Oyuncu ismi boþ olamaz.");
            return;
        }

        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save();

        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.networkAddress = "192.168.0.21"; // Þehir sunucusu IP
        NetworkManager.singleton.StartClient();
    }

    public void OnClick_StartCityServer()
    {
        Debug.Log("Þehir serverý baþlatýlýyor...");
        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.ServerChangeScene("CityScene");
    }
}
