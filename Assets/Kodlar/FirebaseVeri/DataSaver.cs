using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class DataSaver : MonoBehaviour
{
    public long kullaniciSayisi = 0;
    public GameObject BinalarParent;
    public List<BinaVeri> binaVeris; // D��ar�dan atanacak (Inspector ya da ba�ka bir script)

    private DatabaseReference dbRef;

    void GetKullaniciSayisi(System.Action<long> callback)
    {
        dbRef.Child("kullanicilar").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Kullan�c� say�s� al�namad�: " + task.Exception);
                callback?.Invoke(0);
                return;
            }

            kullaniciSayisi = task.Result.ChildrenCount;
            callback?.Invoke(kullaniciSayisi);
        });
    }

    private void Awake()
    {
        BinalarVeri binaVeriScript = FindFirstObjectByType<BinalarVeri>();
        if (binaVeriScript != null)
        {
            binaVeris = binaVeriScript.binaVeris;
        }
    }
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                        {
                            if (task.Result == DependencyStatus.Available)
                            {
                                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                                Debug.Log("Firebase ba�lant�s� ba�ar�l�.");
                                KaydetVeriyi();
                            }
                            else
                            {
                                Debug.LogError("Firebase ba�lant�s� ba�ar�s�z: " + task.Result);
                            }
                        });
    }

        void KaydetVeriyi()
        {
            if (binaVeris == null || binaVeris.Count == 0)
            {
                Debug.LogWarning("Kaydedilecek bina verisi yok.");
                return;
            }
        GetKullaniciSayisi(kullaniciSayisi =>
        {
            string json = JsonUtility.ToJson(new BinaVeriWrapper { binalar = binaVeris });

            string yeniKullaniciId = "kullanici" + kullaniciSayisi;

            dbRef.Child("kullanicilar").Child(yeniKullaniciId).Child("koy").SetRawJsonValueAsync(json)
                .ContinueWithOnMainThread(t =>
                {
                    if (t.IsCompleted)
                        Debug.Log("Veriler Firebase'e kaydedildi.");
                    else
                        Debug.LogError("Firebase'e veri kaydedilemedi: " + t.Exception);
                });
        });
        string json = JsonUtility.ToJson(new BinaVeriWrapper { binalar = binaVeris });

            dbRef.Child("kullanicilar").Child("kullanici" + kullaniciSayisi).Child("koy").SetRawJsonValueAsync(json)
                .ContinueWithOnMainThread(t =>
                {
                    if (t.IsCompleted)
                        Debug.Log("Veriler Firebase'e kaydedildi.");
                    else
                        Debug.LogError("Firebase'e veri kaydedilemedi: " + t.Exception);
                });
        }
    }
    [System.Serializable]
    public class BinaVeriWrapper
    {
        public List<BinaVeri> binalar;
    }
