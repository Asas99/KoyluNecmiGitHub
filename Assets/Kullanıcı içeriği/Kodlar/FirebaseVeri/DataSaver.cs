using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class DataSaver : MonoBehaviour
{
    public long kullaniciSayisi = 0;
    public int UserPlayedBefore; //0-false, 1-true
    public GameObject BinalarParent;
    public List<BinaVeri> binaVeris; // Dýþarýdan atanacak (Inspector ya da baþka bir script)

    private DatabaseReference dbRef;

    void GetKullaniciSayisi(System.Action<long> callback)
    {
        dbRef.Child("kullanicilar").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Kullanýcý sayýsý alýnamadý: " + task.Exception);
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
        if (PlayerPrefs.HasKey("UserSaved"))
        {
            UserPlayedBefore = PlayerPrefs.GetInt("UserSaved");
        }
    }
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                        {
                            if (task.Result == DependencyStatus.Available)
                            {
                                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                                //Debug.Log("Firebase baðlantýsý baþarýlý.");
                                KaydetVeriyi();
                            }
                            else
                            {
                                Debug.LogError("Firebase baðlantýsý baþarýsýz: " + task.Result);
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
            if(UserPlayedBefore == 0)
            {
                string yeniKullaniciId = "kullanici" + kullaniciSayisi;
                dbRef.Child("kullanicilar").Child(yeniKullaniciId).Child("koy").SetRawJsonValueAsync(json)
                    .ContinueWithOnMainThread(t =>
                    {
                        if (t.IsCompleted)
                            Debug.Log("Veriler Firebase'e kaydedildi.");
                        else
                            Debug.LogError("Firebase'e veri kaydedilemedi: " + t.Exception);
                    });
                UserPlayedBefore = 1;
                PlayerPrefs.SetInt("UserSaved", UserPlayedBefore);
            }

        });


        //string json = JsonUtility.ToJson(new BinaVeriWrapper { binalar = binaVeris });

            //dbRef.Child("kullanicilar").Child(yeniKullaniciId).Child("koy").SetRawJsonValueAsync(json)
            //    .ContinueWithOnMainThread(t =>
            //    {
            //        if (t.IsCompleted)
            //            Debug.Log("Veriler Firebase'e kaydedildi.");
            //        else
            //            Debug.LogError("Firebase'e veri kaydedilemedi: " + t.Exception);
            //    });
    }
    }
    [System.Serializable]
    public class BinaVeriWrapper
    {
        public List<BinaVeri> binalar;
    }
