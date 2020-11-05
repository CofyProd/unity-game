/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes) and MMI Teaching (IUT Bordeaux Montaigne)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to be sure that a NPC will appear behind the player
// when the player is under him, and in front of the player when to player
// is above him.
public class PositionRegardingPlayer : MonoBehaviour {
    SpriteRenderer m_renderer;

	// Use this for initialization
	void Start () {
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();

            if (playerRenderer != null)
            {
                if (this.transform.position.y < player.transform.position.y)
                {
                    m_renderer.sortingOrder = playerRenderer.sortingOrder + 1;
                } else
                {
                    m_renderer.sortingOrder = playerRenderer.sortingOrder - 1;
                }
            }
        }
	}
}
