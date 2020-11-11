/* Author : Raphaël Marczak - 2018/2020, for MIAMI Teaching (IUT Tarbes) and MMI Teaching (IUT Bordeaux Montaigne)
 * 
 * This work is licensed under the CC0 License. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Represents the cardinal directions (South, North, West, East)
public enum CardinalDirections { CARDINAL_S, CARDINAL_N, CARDINAL_W, CARDINAL_E };

public class PlayerBehavior : MonoBehaviour
{
    public Death DeathScript;

    public float m_speed = 1f; // Speed of the player when he moves
    private CardinalDirections m_direction; // Current facing direction of the player

    public Sprite m_frontSprite = null;
    public Sprite m_leftSprite = null;
    public Sprite m_rightSprite = null;
    public Sprite m_backSprite = null;
    public Animator animator;
    public AudioSource pas_herbe;
    public AudioSource pas_beton;
    private AudioSource player;
    public AudioSource sortie_camp;
    public AudioSource ville;
    public AudioSource montagne;
    public AudioSource bar;

    public GameObject m_map = null;
    public DialogManager m_dialogDisplayer;

    private Dialog m_closestNPCDialog;
    public GameObject depart;

    Rigidbody2D m_rb2D;
    SpriteRenderer m_renderer;

    void Awake()
    {
        player = pas_herbe;
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
        m_closestNPCDialog = null;
    }

    // This update is called at a very precise and constant FPS, and
    // must be used for physics modification
    // (i.e. anything related with a RigidBody)
    void FixedUpdate()
    {
        // If a dialog is on screen, the player should not be updated
        // If the map is displayed, the player should not be updated
        if (m_dialogDisplayer.IsOnScreen() || m_map.activeSelf)
        {
            return;
        }

        // Moves the player regarding the inputs
        Move();
    }

    private void Move()
    {
        float horizontalOffset = Input.GetAxis("Horizontal");
        float verticalOffset = Input.GetAxis("Vertical");

        // Translates the player to a new position, at a given speed.
        Vector2 newPos = new Vector2(transform.position.x + horizontalOffset * m_speed,
                                     transform.position.y + verticalOffset * m_speed);
        m_rb2D.MovePosition(newPos);
        animator.SetFloat("Speed", verticalOffset);
        animator.SetFloat("SpeedHorizontal", horizontalOffset);
        animator.SetInteger("SpeedHorizontal 0", Mathf.RoundToInt(horizontalOffset));
        animator.SetInteger("Speed 0", Mathf.RoundToInt(verticalOffset));

        // Computes the player main direction (North, Sound, East, West)
        if (Mathf.Abs(horizontalOffset) > Mathf.Abs(verticalOffset))
        {
            if (horizontalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_E;
                animator.SetBool("isRight", true);
                animator.SetBool("isFront", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isLeft", false);
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_W;
                animator.SetBool("isLeft", true);
                animator.SetBool("isFront", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isRight", false);
            }
        }
        else if (Mathf.Abs(horizontalOffset) < Mathf.Abs(verticalOffset))
        {
            if (verticalOffset > 0)
            {
                m_direction = CardinalDirections.CARDINAL_N;
                animator.SetBool("isBack", true);
                animator.SetBool("isFront", false);
                animator.SetBool("isRight", false);
                animator.SetBool("isLeft", false);
            }
            else
            {
                m_direction = CardinalDirections.CARDINAL_S;
                animator.SetBool("isFront", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isLeft", false);
            }
        }
    }


    // This update is called at the FPS which can be fluctuating
    // and should be called for any regular actions not based on
    // physics (i.e. everything not related to RigidBody)
    private void Update()
    {

        // If the player presses M, the map will be activated if not on screen
        // or desactivated if already on screen
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_map.SetActive(!m_map.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // If a dialog is on screen, the player should not be updated
        // If the map is displayed, the player should not be updated
        if (m_dialogDisplayer.IsOnScreen() || m_map.activeSelf)
        {
            return;
        }

        ChangeSpriteToMatchDirection();

        // If the player presses SPACE, then two solution
        // - If there is a dialog ready to be displayed (i.e. the player is closed to a NPC)
        //   then a dialog is set to the dialogManager
        // - If not, then the player will shoot a fireball
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_closestNPCDialog != null)
            {
                m_dialogDisplayer.SetDialog(m_closestNPCDialog.GetDialog());
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.Play();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            player.Stop();
        }
            
        }
    }

    // Changes the player sprite regarding it position
    // (back when going North, front when going south, right when going east, left when going west)
    private void ChangeSpriteToMatchDirection()
    {
        if (m_direction == CardinalDirections.CARDINAL_N)
        {
            m_renderer.sprite = m_backSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_S)
        {
            m_renderer.sprite = m_frontSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_E)
        {
            m_renderer.sprite = m_rightSprite;
        }
        else if (m_direction == CardinalDirections.CARDINAL_W)
        {
            m_renderer.sprite = m_leftSprite;
        }
    }


    // This is automatically called by Unity when the gameObject (here the player)
    // enters a trigger zone. Here, two solutions
    // - the player is in an NPC zone, then he grabs the dialog information ready to be
    //   displayed when SPACE will be pressed
    // - the player is in an instantDialog zone, then he grabs the dialog information and
    //   displays it instantaneously
    private void OnTriggerEnter2D(Collider2D collision)
    {  
        if (collision.tag == "NPC")
        {
            m_closestNPCDialog = collision.GetComponent<Dialog>();
        }
        else if (collision.tag == "InstantDialog")
        {
            Dialog instantDialog = collision.GetComponent<Dialog>();
            if (instantDialog != null)
            {
                m_dialogDisplayer.SetDialog(instantDialog.GetDialog());
            }
        }
        else if (collision.tag == "Police")
        {
            // DeathScript.KillPlayer();
            SceneManager.LoadScene(2); // GameOver
            Debug.Log("Afficher Death Screen");
        }
    }

    // This is automatically called by Unity when the gameObject (here the player)
    // leaves a trigger zone. Here, two solutions
    // - the player was in an NPC zone, then the dialog information is removed
    // - the player was in an instantDialog zone, then the instantDialog is destroyed
    //   (as it has been displayed, and must only be displayed once)
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            m_closestNPCDialog = null;
        }
        else if (collision.tag == "InstantDialog")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "entree ville")
        {
            player = pas_beton;
            pas_herbe.Stop();
            sortie_camp.Stop();
            ville.Play();
            bar.Stop();

        }
        else if (collision.tag == "entree montagne")
        {
            ville.Stop();
            montagne.Play();
            player = pas_herbe;
            pas_beton.Stop();
        }
        else if (collision.tag == "entree bar")
        {
            ville.Stop();
            bar.Play();
        }
    }
}
