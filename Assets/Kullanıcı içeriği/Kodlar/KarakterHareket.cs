using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarakterHareket : OrtakÖzellikler, IHareket
{
    public int Para;
    public int Can;
    [SerializeField]
    private float x, y;
    public float XScale;

    private void Start()
    {
        XScale = transform.localScale.x;

    }

    public void Yürü()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(x, y, 0) * YürümeHýzý * Time.deltaTime;

    }

    public void AnimasyonuDeðiþtir()
    {
        if (animator != null)
        {
            //print("X:" + x + ", Y:" + y);
            animator.SetInteger("Yan_input", (int)x);
            animator.SetInteger("Dikey_input", (int)y);

            //Hareket konrolü
            bool hareketVar = (x != 0 || y != 0);
            animator.SetBool("Hareket Halinde", hareketVar);

            if (y == 1)
            {
                animator.SetBool("Arka", true);
                animator.SetBool("Ön", false);
                animator.SetBool("Sol", false);
                animator.SetBool("Sað", false);
            }
            if (y == -1)
            {
                animator.SetBool("Arka", false);
                animator.SetBool("Ön", true);
                animator.SetBool("Sol", false);
                animator.SetBool("Sað", false);
            }
            if (x == 1)
            {
                if (XScale < 0)
                {
                    XScale = -XScale;
                }
                animator.SetBool("Sol", false);
                animator.SetBool("Sað", true);
                animator.SetBool("Arka", false);
                animator.SetBool("Ön", false);
                transform.localScale = new Vector3(XScale, transform.localScale.y, transform.localScale.z);
            }
            if (x == -1)
            {
                if (XScale > 0)
                {
                    XScale = -XScale;
                }
                animator.SetBool("Sað", false);
                animator.SetBool("Sol", true);
                animator.SetBool("Arka", false);
                animator.SetBool("Ön", false);
                transform.localScale = new Vector3(XScale, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void Update()
    {
            if (animator != null)
            {
                    Yürü();
                    AnimasyonuDeðiþtir();
            }
    }
}

