using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarakterHareket : Ortak�zellikler, IHareket
{
    public int Para;
    public int Can;
    public bool OnLobby;
    [SerializeField]
    private float x, y;
    public GameObject PosText;
    public void Y�r�()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(x, y, 0) * Y�r�meH�z� * Time.deltaTime;

    }

    public void AnimasyonuDe�i�tir()
    {
        if (animator != null)
        {       
            //print("X:" + x + ", Y:" + y);
            animator.SetInteger("Yan_input", (int)x);
            animator.SetInteger("Dikey_input", (int)y);

            //Hareket konrol�
            bool hareketVar = (x != 0 || y != 0);
            animator.SetBool("Hareket Halinde", hareketVar);

            if (y == 1)
            {
                animator.SetBool("Arka", true);
                animator.SetBool("�n", false);
                animator.SetBool("Sol", false);
                animator.SetBool("Sa�", false);
            }
            if (y == -1)
            {
                animator.SetBool("Arka", false);
                animator.SetBool("�n", true);
                animator.SetBool("Sol", false);
                animator.SetBool("Sa�", false);
            }
            if (x == 1)
            {
                animator.SetBool("Sol", false);
                animator.SetBool("Sa�", true);
                animator.SetBool("Arka", false);
                animator.SetBool("�n", false);
                transform.localEulerAngles = new Vector3(0, 0,
    0);
            }
            if (x == -1)
            {
                animator.SetBool("Sa�", false);
                animator.SetBool("Sol", true);
                animator.SetBool("Arka", false);
                animator.SetBool("�n", false);
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
        //if (!OnLobby) 
        //{
        //    Y�r�();
        //    AnimasyonuDe�i�tir();
        //}
        //if (OnLobby)
        //{
        //    PhotonView photonView = gameObject.GetComponent<PhotonView>();
        //    if (photonView.IsMine)
        //    {
        //        PosText.GetComponent<TextMesh>().text = gameObject.transform.position.ToString() + PhotonNetwork.CurrentRoom.PlayerCount;
        //        print(gameObject.name);
        //        Y�r�();
        //        AnimasyonuDe�i�tir();
        //    }
        //}
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Pazar")  // Sadece lobi sahnesindeyse
        {
            PhotonView photonView = GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                Y�r�();
                AnimasyonuDe�i�tir();
            }
        }
        else
        {
            Y�r�();
            AnimasyonuDe�i�tir();   // Lobi d��� sahnelerde hareket ve animasyon kontrol� yok
            // �stersen ba�ka i�lemler burada olabilir
        }
    }
}
