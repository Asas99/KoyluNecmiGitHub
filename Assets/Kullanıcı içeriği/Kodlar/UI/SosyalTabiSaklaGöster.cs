using UnityEngine;
using UnityEngine.UI;

public class SosyalTabiSaklaGöster : MonoBehaviour
{
    public Animator animator;
    public bool SaklıMı;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SaklaGöster()
    {
        SaklıMı = !SaklıMı;
        print(SaklıMı);
        animator.SetBool("Göster", !SaklıMı);
    }
}
