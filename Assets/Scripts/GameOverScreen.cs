using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene("");
    }
    public void GoHome()
    {
        SceneManager.LoadScene("Menu");
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
