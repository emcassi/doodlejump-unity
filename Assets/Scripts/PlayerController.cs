using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce;
    public float movementSpeed;
    public float maxVel;
    public Camera camera;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool facingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
        
        Vector2 screenBounds = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 screenOrigin = camera.ScreenToWorldPoint(Vector2.zero);

        if (transform.position.x < screenOrigin.x)
            transform.position = new Vector3(screenBounds.x, transform.position.y, transform.position.z);

        if (transform.position.x > screenBounds.x)
            transform.position = new Vector3(screenOrigin.x, transform.position.y, transform.position.z);

        sr.flipX = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.CompareTo("Platform") == 0)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
