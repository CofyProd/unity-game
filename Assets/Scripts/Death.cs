using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is used to teleport the player from one scene to another
// If the scene is different, then the previous scene is disabled and the new
// one is enabled ; and the map indication is updated accordingly
public class Death : MonoBehaviour
{

    private GameObject m_player = null;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            KillPlayer();
        }

    }

    public void KillPlayer()
    {
        // Debug.Log("Hello");
        SceneManager.LoadScene(2); // GameOver
        // Debug.Log(SceneManager.sceneCount);
        // SceneManager.LoadScene(2); // GameOver
    }
}
