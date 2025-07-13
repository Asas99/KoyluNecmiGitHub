using System;
using UnityEngine;
using UnityEngine.UI;

public class KarakterHareket : OrtakÖzellikler, IHareket
{
    public int Para;
    public int Can;
    [SerializeField]
    private float x, y;

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
                animator.SetBool("Sol", false);
                animator.SetBool("Sað", true);
                animator.SetBool("Arka", false);
                animator.SetBool("Ön", false);
                transform.localEulerAngles = new Vector3(0, 0,
    0);
            }
            if (x == -1)
            {
                animator.SetBool("Sað", false);
                animator.SetBool("Sol", true);
                animator.SetBool("Arka", false);
                animator.SetBool("Ön", false);
                transform.localEulerAngles = new Vector3(0, 180,
                    0);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Yürü();
        AnimasyonuDeðiþtir();
    }
}
