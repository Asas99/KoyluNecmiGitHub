using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinalarVeri : MonoBehaviour
{
    public List<BinaVeri> binaVeris = new List<BinaVeri>();
    public Transform BinalarParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Kaydet();
        Yukle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kaydet()
    {
        binaVeris.Clear();

        for (int i = 0; i < BinalarParent.childCount; i++)
        {
            Transform child = BinalarParent.GetChild(i);

            BinaVeri veri = new BinaVeri
            {
                BinaTuru = child.name,
                konum = new Konum
                {
                    x = child.position.x,
                    y = child.position.y,
                    z = child.position.z
                },
                rotasyon = new Rotasyon
                {
                    x = child.eulerAngles.x,
                    y = child.eulerAngles.y,
                    z = child.eulerAngles.z
                }
            };

            binaVeris.Add(veri);
        }

        Debug.Log("Bina verileri listeye kaydedildi. Toplam: " + binaVeris.Count);
    }
    public void Yukle()
    {
        // Önce var olan binalarý temizlemek istersen:
        for (int i = BinalarParent.childCount - 1; i >= 0; i--)
        {
            Destroy(BinalarParent.GetChild(i).gameObject);
        }

        // Kaydedilen veriye göre yeni binalarý oluþtur
        foreach (var bina in binaVeris)
        {
            GameObject prefab = Resources.Load<GameObject>("Binalar/" + bina.BinaTuru);
            if (prefab != null)
            {
                Vector3 konum = new Vector3(bina.konum.x, bina.konum.y, bina.konum.z);
                Quaternion rotasyon = Quaternion.Euler(bina.rotasyon.x, bina.rotasyon.y, bina.rotasyon.z);

                Instantiate(prefab, konum, rotasyon, BinalarParent);
            }
            else
            {
                Debug.LogWarning("Prefab bulunamadý: " + bina.BinaTuru);
            }
        }

        Debug.Log("Binalar sahneye yüklendi.");
    }

}

[System.Serializable]
public class BinaVeri
{
    public string BinaTuru;
    public Konum konum;
    public Rotasyon rotasyon;
}

[System.Serializable]
public class Konum
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Rotasyon
{
    public float x;
    public float y;
    public float z;
}