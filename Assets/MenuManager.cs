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
    public Button cityServerButton; // Sadece �ehir sunucusu i�in (build'de g�r�n�r)

    const string PlayerNameKey = "PlayerName";
    const string HasVillageKey = "HasVillage";

    void Start()
    {
        // Oyuncu ismi varsa otomatik doldur
        if (PlayerPrefs.HasKey(PlayerNameKey))
            playerNameInput.text = PlayerPrefs.GetString(PlayerNameKey);

        // Daha �nce k�y olu�turulmu�sa Devam Et aktif olur
        continueButton.interactable = PlayerPrefs.HasKey(HasVillageKey);

        // �ehir server butonunu sadece server build'de g�ster
#if UNITY_EDITOR
        cityServerButton.gameObject.SetActive(false);
#else
        cityServerButton.gameObject.SetActive(IsCityServerBuild());
#endif
    }

    bool IsCityServerBuild()
    {
        // �rne�in PlayerPrefs veya komut sat�r� arg�man�na g�re kontrol edebilirsin
        return true; // �imdilik hep true, istersen �zelle�tirebiliriz
    }

    public void OnClick_NewVillage()
    {
        string playerName = playerNameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Oyuncu ismi bo� olamaz.");
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
            Debug.LogWarning("Daha �nce k�y olu�turulmam��.");
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
            Debug.LogWarning("Oyuncu ismi bo� olamaz.");
            return;
        }

        PlayerPrefs.SetString(PlayerNameKey, playerName);
        PlayerPrefs.Save();

        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.networkAddress = "192.168.0.21"; // �ehir sunucusu IP
        NetworkManager.singleton.StartClient();
    }

    public void OnClick_StartCityServer()
    {
        Debug.Log("�ehir server� ba�lat�l�yor...");
        if (NetworkManager.singleton == null)
        {
            Debug.LogError("NetworkManager sahnede eksik!");
            return;
        }

        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.ServerChangeScene("CityScene");
    }
}
