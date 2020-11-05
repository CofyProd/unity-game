/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes) and MMI Teaching (IUT Bordeaux Montaigne)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehavior : MonoBehaviour {
    Rigidbody2D m_rb2D;

    float m_launchedTime; // Absolute time (in sec.) when the fireball has been launched
    float m_fireDuration = 2f; // Time (in sec.) after which the fireball should be destroyed

    float m_speed = 100f; // Speed of the fireball

    void Awake () {
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        m_launchedTime = Time.realtimeSinceStartup;
    }

    public void Launch(Vector2 direction)
    {
        m_rb2D.AddForce(direction.normalized * m_speed, ForceMode2D.Impulse);
    }
	
	void Update () {
        // Checks if the fireball should remain on screen
        // or if the life time has been reached
		if (Time.realtimeSinceStartup > m_launchedTime + m_fireDuration)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroys the fireball when it hits something, except the player or another fireball
        // (to prevent the fireball to be destroyed as soon as it is created)
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Fireball")
        {
            Destroy(gameObject);
        } else
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
