using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float jumpForce;
    public float movementSpeed;
    public float maxVel;
    public Camera camera;
    public float maxHeight;
    public Vector2 screenBounds, screenOrigin;
    public TMP_Text scoreLabel;
    public GameObject pausePanel, gameOverPanel;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight = true, alive = true, paused = false;
    private int score = 0;
    private Vector2 pausedVel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive && !paused)
        {
            var hor = Input.GetAxisRaw("Horizontal") * movementSpeed;
            rb.velocity = new Vector2(hor, rb.velocity.y);

            facingRight = hor switch
            {
                > 0 => true,
                < 0 => false,
                _ => facingRight
            };

            if (rb.velocity.magnitude > maxVel)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
            }

            screenBounds = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            screenOrigin = camera.ScreenToWorldPoint(Vector2.zero);

            Vector3 currentPos = transform.position;

            if (currentPos.x < screenOrigin.x)
                transform.position = new Vector3(screenBounds.x, currentPos.y, currentPos.z);

            if (transform.position.x > screenBounds.x)
                transform.position = new Vector3(screenOrigin.x, currentPos.y, currentPos.z);

            if (transform.position.y > maxHeight)
                maxHeight = transform.position.y;

            Vector3 camPos = camera.transform.position;

            if (maxHeight > camera.transform.position.y)
                camera.transform.position = new Vector3(camPos.x,
                    Mathf.Lerp(camPos.y, maxHeight, 0.5f), camPos.z);

            if (currentPos.y < screenOrigin.y)
                Kill();

            sr.flipX = !facingRight;

            score = (int)maxHeight;
            scoreLabel.text = score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Platform") == 0 && alive && !paused)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void Kill()
    {
        alive = false;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        paused = !paused;  
        pausePanel.SetActive(paused);

        if (paused)
        {
            pausedVel = rb.velocity;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = pausedVel;
            rb.gravityScale = 1;
        }

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
