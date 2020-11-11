using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cactus : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject message;
    public GameObject dialog;
    private bool once = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            message.SetActive(true);
            once = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                message.SetActive(false);
                if (once == false)
                {
                    dialog.SetActive(true);
                    once = true;
                }else{
                    dialog.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        message.SetActive(false);
        dialog.SetActive(false);
    }
}
