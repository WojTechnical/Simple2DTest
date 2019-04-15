using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    public float moveSpeed = 10f;
    public float jumpForce = 500f;
    public LayerMask whatIsGround;
    public Text text;

    bool isGrounded = false;
    bool jumpRequest = false;
    float horizontalValue = 0f;
    float groundedSkin = 0.05f;

    Vector2 playerSize;
    Vector2 boxSize;

    private void Awake()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        //Debug.Log(playerSize.ToString());
        //Debug.Log(boxSize.ToString());
    }

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        isGrounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, whatIsGround) != null;

        text.text = "Is grounded: " +  (isGrounded?"yes":"no");


        bool down = Input.GetButtonDown("Jump");
        //bool held = Input.GetButton("Jump");
        //bool up = Input.GetButtonUp("Jump");

        if (down && isGrounded)
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(horizontalValue * moveSpeed, player.velocity.y);

        if (jumpRequest)
        {
            player.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);

            jumpRequest = false;
        }

        player.rotation = 0f;

    }
}