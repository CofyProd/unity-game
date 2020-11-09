﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
