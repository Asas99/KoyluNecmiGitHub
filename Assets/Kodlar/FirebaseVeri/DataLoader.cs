using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using System.Collections.Generic;

public class DataLoader : MonoBehaviour
{
    private DatabaseReference dbRef;
    public Transform binalarParent; // Sahnedeki binalarýn atanacaðý ana obje

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                string kullaniciId = PlayerPrefs.GetString("kullaniciId", ""); // default boþ
                if (!string.IsNullOrEmpty(kullaniciId))
                    VeriyiCek(kullaniciId);
                else
                    Debug.LogWarning("Kullanýcý ID ayarlanmamýþ.");
            }
            else
            {
                Debug.LogError("Firebase baðlantýsý baþarýsýz: " + task.Result);
            }
        });
    }

    public void VeriyiCek(string kullaniciId)
    {
        dbRef.Child("kullanicilar").Child(kullaniciId).Child("koy").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Veri çekilemedi: " + task.Exception);
                return;
            }

            string json = task.Result.GetRawJsonValue();

            if (!string.IsNullOrEmpty(json))
            {
                BinaVeriWrapper wrapper = JsonUtility.FromJson<BinaVeriWrapper>(json);
                Debug.Log("Firebase'den çekilen bina sayýsý: " + wrapper.binalar.Count);
                SahneyeYukle(wrapper.binalar);
            }
            else
            {
                Debug.LogWarning("Veri boþ.");
            }
        });
    }

    private void SahneyeYukle(List<BinaVeri> binalar)
    {
        // 1. Eski binalarý sil
        foreach (Transform child in binalarParent)
        {
            Destroy(child.gameObject);
        }

        // 2. Yeni binalarý instantiate et
        foreach (var bina in binalar)
        {
            GameObject prefab = Resources.Load<GameObject>("Binalar/" + bina.BinaTuru);
            if (prefab != null)
            {
                Vector3 pos = new Vector3(bina.konum.x, bina.konum.y, bina.konum.z);
                Quaternion rot = Quaternion.Euler(bina.rotasyon.x, bina.rotasyon.y, bina.rotasyon.z);
                Instantiate(prefab, pos, rot, binalarParent);
            }
            else
            {
                Debug.LogWarning("Prefab bulunamadý: " + bina.BinaTuru);
            }
        }

        Debug.Log("Sahneye binalar yüklendi.");
    }
}
