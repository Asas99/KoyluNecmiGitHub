using UnityEditor.Callbacks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArabaKullan : MonoBehaviour
{
    [Header("Hız ve Kuvvetler")]
    public float maxIleriHiz = 15f;
    public float maxGeriHiz = -6f;
    public float ivme = 20f;
    public float frenKuvveti = 30f;

    [Header("Kayma Ayarları")]
    public float SürtünmeKatsayısı = 0.2f; // normalde yan kayma azaltılır

    [Header("Direksiyon Ayarları")]
    public float direksiyonHassasiyeti = 100f;   // direksiyonun dönme hızı
    public float direksiyonAcisi = 0f;          // anlık direksiyon açısı

    [Header("Vites Sistemi")]  
    public SürüşTipi sürüşTipi = SürüşTipi.P;
    public enum SürüşTipi { P, R, N, D }


    private Rigidbody2D rb;
    [SerializeField]
    private float inputGaz;
    [SerializeField]
    private float inputYon;
    private ArabayaİnBin ArabayaİnBin;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 2D top-down olduğundan yerçekimi olmasın
        ArabayaİnBin = GameObject.FindFirstObjectByType<ArabayaİnBin>();
    }

    void Update()
    {
        // Input al
        inputGaz = Input.GetAxisRaw("Vertical");   // W/S
        inputYon = Input.GetAxisRaw("Horizontal"); // A/D

        // Vites değişimi (scroll ile)
        int vites = (int)sürüşTipi;
        vites += (int)Input.mouseScrollDelta.y;
        vites = Mathf.Clamp(vites, 0, 3);
        sürüşTipi = (SürüşTipi)vites;

        #region Direksiyon
        // Direksiyon açısını ayarla
        if(ArabayaİnBin.ArabadaMı)
        if (inputYon != 0)
        {
            direksiyonAcisi += -inputYon * direksiyonHassasiyeti * Time.deltaTime;
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Hareket etme
            if (ArabayaİnBin.ArabadaMı)
            // Park → tamamen durdur
            if (sürüşTipi == SürüşTipi.P)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                return;
            }

            // İleri ve yan hız bileşenlerini ayır
            Vector2 ileri = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
            Vector2 yan = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

            // Motor kuvvetleri
            if (sürüşTipi == SürüşTipi.D)
            {
                rb.AddForce(transform.up * inputGaz * ivme);
            }
            else if (sürüşTipi == SürüşTipi.R)
            {
                if (inputGaz > 0)
                {
                    rb.AddForce(-transform.up * inputGaz * ivme);
                }
            }
            //Fren
            else if (inputGaz < 0) // fren
            {
            if (Mathf.Abs(rb.linearVelocity.y) >= 0.2f)
            {
                rb.AddForce(-rb.linearVelocity.normalized * frenKuvveti);
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }
            }
            
            //Sürtünme
            if (rb.linearVelocity.magnitude > 0.01f)
            {
                rb.AddForce(-rb.linearVelocity.normalized * SürtünmeKatsayısı);
            }
            else
            {
                // Çok yavaşsa hızı tamamen sıfırla
                rb.linearVelocity = Vector2.zero;
            }


        // Maksimum hız sınırı
        float hiz = Vector2.Dot(rb.linearVelocity, transform.up);
            if (sürüşTipi == SürüşTipi.D && hiz > maxIleriHiz)
                rb.linearVelocity = transform.up * maxIleriHiz;
            if (sürüşTipi == SürüşTipi.R && hiz < maxGeriHiz)
                rb.linearVelocity = transform.up * maxGeriHiz;
        #endregion

        #region Direksiyon
        // Araba hareket ediyorsa direksiyon açısına göre yönünü değiştir
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            // Mevcut rotasyon
            float mevcutRotasyon = rb.rotation;

            // Hedef açıyı direksiyonAcisi olarak al
            float hedefRotasyon = direksiyonAcisi;

            // Kademeli olarak dön (saniyede 100 derece gibi)
            rb.rotation = Mathf.MoveTowards(mevcutRotasyon, hedefRotasyon, 100f * Time.fixedDeltaTime);
        }
        #endregion
    }
}
