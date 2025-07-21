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
        if (PlayerPrefs.HasKey(PlayerNameKey))
            playerNameInput.text = PlayerPrefs.GetString(PlayerNameKey);

        continueButton.interactable = PlayerPrefs.HasKey(HasVillageKey);
    }

    public void OnClick_NewVillage()
    {
        if (string.IsNullOrEmpty(playerNameInput.text)) return;

        PlayerPrefs.SetString(PlayerNameKey, playerNameInput.text);
        PlayerPrefs.SetInt(HasVillageKey, 1);
        PlayerPrefs.Save();

        NetworkManager.singleton.StartHost(); // Oyuncu host olur
    }

    public void OnClick_Continue()
    {
        if (!PlayerPrefs.HasKey(HasVillageKey)) return;

        NetworkManager.singleton.StartHost(); // Kendi köyüne devam
    }

    public void OnClick_EnterCity()
    {
        if (string.IsNullOrEmpty(playerNameInput.text)) return;

        PlayerPrefs.SetString(PlayerNameKey, playerNameInput.text);
        PlayerPrefs.Save();

        NetworkManager.singleton.networkAddress = "127.0.0.1"; // Þehir server IP’si
        NetworkManager.singleton.StartClient();
    }
}
