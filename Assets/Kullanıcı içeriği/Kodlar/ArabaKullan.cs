using UnityEngine;

public class ArabaKullan : MonoBehaviour
{
    [Range(-1000, 1000)]
    public float GeriMaxSürüşHızı, MaxSürüşHızı, MaxDönüşHızı;
    public float SürüşHızı, DönüşHızı;
    public float GeriÇizgiselİvme, Çizgiselİvme, Açısalİvme;
    public float ÇizgiselSürtünme, AçısalSürtünme;
    public float DurmaHızı;
    public bool KullanılıyorMu;
    public enum SürüşTipi { P, R, N, D };
    public SürüşTipi sürüşTipi;

    private ArabayaİnBin ArabayaİnBin;

    void Start()
    {
        ArabayaİnBin = GameObject.FindFirstObjectByType<ArabayaİnBin>();
    }

    void Update()
    {
        //Hızları sınırla
        SürüşHızı = Mathf.Clamp(SürüşHızı, GeriMaxSürüşHızı, MaxSürüşHızı);
        DönüşHızı = Mathf.Clamp(DönüşHızı, -MaxDönüşHızı, MaxDönüşHızı);

        //Arabada mı değilmi kontrolü
        if (ArabayaİnBin != null && ArabayaİnBin.Araba == gameObject)
        {
            KullanılıyorMu = ArabayaİnBin.ArabadaMı;
        }
        else
        {
            KullanılıyorMu = false;
        }

        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (KullanılıyorMu)
        {
            #region Hareket ve dönüş
                // Dönüş
                if (x == 0)
                {
                    DönüşHızı = Mathf.MoveTowards(DönüşHızı, 0, AçısalSürtünme * Time.deltaTime);
                }
                else
                {
                    DönüşHızı += Açısalİvme * Time.deltaTime * -x;
                }

                // İleri veya fren (geri tuşuyla)
                if (y == 0)
                {
                   SürüşHızı = Mathf.MoveTowards(SürüşHızı, 0, ÇizgiselSürtünme * Time.deltaTime);
                }
                else if (y > 0)
                {
                    if (sürüşTipi == SürüşTipi.D)
                    {
                        SürüşHızı += Çizgiselİvme * Time.deltaTime * y;
                    }
                    if (sürüşTipi == SürüşTipi.R)
                    {
                        SürüşHızı += GeriÇizgiselİvme * Time.deltaTime * y;
                    }
                }
                //Geri tuşu fren görevi görecek
                else if (y < 0)
                {
                    if (sürüşTipi == SürüşTipi.D || sürüşTipi == SürüşTipi.R)
                    {
                        SürüşHızı = Mathf.MoveTowards(SürüşHızı, 0, ÇizgiselSürtünme + (DurmaHızı * Time.deltaTime));
                    }
                }
            #endregion

            #region Vites değişimi
            // Önce sürüşTipi enumunu int'e çevirip scroll ile artır/azalt
            int vites = (int)sürüşTipi;
                vites += (int)Input.mouseScrollDelta.y; // scroll yukarı: +1, scroll aşağı: -1

                // Enum aralığında kalmasını sağla (0..3)
                vites = Mathf.Clamp(vites, 0, 3);

                // Tekrar enum tipine çevir
                sürüşTipi = (SürüşTipi)vites;
            #endregion

            // Hareket ve dönüş
            transform.Translate(transform.up * Time.deltaTime * SürüşHızı, Space.World);
            transform.Rotate(Vector3.forward * DönüşHızı * Time.deltaTime);
        }
    }
}
