/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes) and MMI Teaching (IUT Bordeaux Montaigne)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to teleport the player from one scene to another
// If the scene is different, then the previous scene is disabled and the new
// one is enabled ; and the map indication is updated accordingly
public class Teleport : MonoBehaviour {
    public GameObject m_teleportTo = null;
    
    private GameObject m_player = null;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.tag == "Player")
        {
            TeleportPlayer();
        }

    }

    private void TeleportPlayer()
    {
        if (m_teleportTo != null)
        {
            if (this.transform.parent != null)
            {
                this.transform.parent.gameObject.SetActive(false);
                m_teleportTo.transform.parent.gameObject.SetActive(true);

                m_player.transform.position = m_teleportTo.transform.position;
            }
        }
    }
}
