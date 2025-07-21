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

    const string PlayerNameKey = "PlayerName";
    const string HasVillageKey = "HasVillage";

    void Start()
    {
        // Oyuncu ismi varsa otomatik doldur
        if (PlayerPrefs.HasKey(PlayerNameKey))
            playerNameInput.text = PlayerPrefs.GetString(PlayerNameKey);

        // Daha önce köy oluþturulmuþsa Devam Et aktif olsun
        continueButton.interactable = PlayerPrefs.HasKey(HasVillageKey);
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

        NetworkManager.singleton.StartHost(); // Oyuncu host olur (kendi köyünü baþlatýr)
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

        NetworkManager.singleton.StartHost(); // Oyuncu daha önce oluþturduðu köye girer
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

        NetworkManager.singleton.networkAddress = " 192.168.0.21"; // Þehir sunucusunun IP'si
        NetworkManager.singleton.StartClient(); // Þehre baðlan
    }
}
