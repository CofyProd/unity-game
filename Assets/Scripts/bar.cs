using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject message;
    public GameObject dialog;
    private int once = 1;
    public GameObject texte1;
    public GameObject texte2;
    public GameObject texte3;
    public GameObject bar1;
    public Animator animator;
    public Animator animator2;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       Debug.Log(once);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            message.SetActive(true);
            once = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                message.SetActive(false);
                 if (once == 1)
                {   
                    animator.SetBool("parle", true);
                    animator2.SetBool("parle", false);
                    dialog.SetActive(true);
                    texte1.SetActive(true);
                    texte2.SetActive(false);
                    texte3.SetActive(false);
                    once = 2;
                    return;
                } if (once == 2)
                {
                    animator.SetBool("parle", true);
                    animator2.SetBool("parle", false);
                    texte1.SetActive(false);
                    texte2.SetActive(true); 
                    texte3.SetActive(false);
                    once = 3;
                    return;
                }if (once == 3)
                {
                    animator.SetBool("parle", false);
                    animator2.SetBool("parle", true);
                    texte1.SetActive(false);
                    texte2.SetActive(false); 
                    texte3.SetActive(true); 
                    once = 4;
                    return;
                }
                if (once == 4){
                    dialog.SetActive(false);
                    texte2.SetActive(false);
                    texte3.SetActive(false);
                    texte1.SetActive(false);
                }
                
                
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        message.SetActive(false);
        dialog.SetActive(false);
        once = 1;
    }
}
