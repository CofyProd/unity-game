﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debut : MonoBehaviour
{public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerStay2D(Collider2D collision) {  
        if (Input.GetKeyDown(KeyCode.Space)){
                Destroy(gameObject);
                Debug.Log("yes");
            }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            dialog.SetActive(true);
        }
    }
   
}
