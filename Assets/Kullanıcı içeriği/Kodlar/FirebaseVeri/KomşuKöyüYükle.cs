using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class KomşuKöyüYükle : MonoBehaviour
{
    public Text IdText;
    public Text ErrorText;
    [SerializeField]
    private float WaitTime = 3;

    private DatabaseReference dbRef;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
               // Debug.Log("Firebase hazır.");
            }
            else
            {
                Debug.LogError("Firebase başlatılamadı: " + task.Result);
            }
        });
    }

    void Update()
    {
        if (ErrorText.gameObject.activeInHierarchy)
        {
            WaitTime -= Time.deltaTime;
            if (WaitTime <= 0)
            {
                ErrorText.gameObject.SetActive(false);
                WaitTime = 3;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            string kullaniciId = IdText.text.Trim();
            if (!string.IsNullOrEmpty(kullaniciId))
            {
                PlayerPrefs.SetString("kullaniciId", kullaniciId);
                FirebaseIDKontrol(kullaniciId);
            }
            else
            {
                ShowErrorMessage("Lütfen geçerli bir ID gir.");
            }
        }
    }

    void FirebaseIDKontrol(string kullaniciId)
    {
        dbRef.Child("kullanicilar").Child(kullaniciId).Child("koy").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firebase erişim hatası: " + task.Exception);
                ShowErrorMessage("Sunucu hatası.");
                return;
            }

            if (task.Result.Exists)
            {
                Debug.Log("Kullanıcı bulundu, sahneye geçiliyor...");
                SceneManager.LoadScene(1);
            }
            else
            {
                ShowErrorMessage("Belirttiğiniz adda bir kullanıcı yoktur.");
            }
        });
    }

    void ShowErrorMessage(string message)
    {
        ErrorText.text = message;
        ErrorText.gameObject.SetActive(true);
    }
}
