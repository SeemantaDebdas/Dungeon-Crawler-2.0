using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerMovement : MonoBehaviour, IDamagable
{
    //cache reference
    Rigidbody2D rb;
    PlayerAnimation playerAnimation;
    SpriteRenderer spriteRenderer;
    SpriteRenderer swordSpriteRenderer;
    Transform swordSprite;
    AudioSource audi;

    //parameter variables
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] AudioClip jumpAudio;
    [SerializeField] AudioClip attackAudio;
    [SerializeField] AudioClip deathAudio;

    float horizontalInput;

    public int Health { get; set; }
    public int Gems;
    public bool Dead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        swordSprite = transform.GetChild(1);
        swordSpriteRenderer = swordSprite.GetComponent<SpriteRenderer>();
        audi = GetComponent<AudioSource>();

        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead)
            return;
        Move();
        Jump();
        Attack();
        HealthManager();
    }

    void Move()
    {
        horizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");//Input.GetAxisRaw("Horizontal"); 

        SpriteInversion();

        playerAnimation.Move(horizontalInput);

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void SpriteInversion()
    {
        Vector2 temp;

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            swordSpriteRenderer.flipX = false;
            swordSpriteRenderer.flipY = false;

            temp = swordSprite.localPosition;
            temp.x = Mathf.Abs(temp.x);
            swordSprite.localPosition = temp;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            swordSpriteRenderer.flipX = true;
            swordSpriteRenderer.flipY = true;

            temp = swordSprite.localPosition;
            temp.x = -Mathf.Abs(temp.x);
            swordSprite.localPosition = temp;
        }


    }

    void Jump()
    {
        playerAnimation.Jumping(!IsGrounded());

        Color rayColor;
        if (IsGrounded())
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(transform.position, Vector2.down * (transform.localScale.x / 2 + groundDistance), rayColor);


        if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audi.clip = jumpAudio;
            audi.Play();
        }
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            playerAnimation.Attacking();
            audi.clip = attackAudio;
            audi.Play();
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.x / 2 + groundDistance, groundLayer);
        if (hit.collider)
            return true;
        return false;
    }

    public void Damage()
    {
        if (Health < 1)
            return;
        Health--;
        UIManager.Instance.UpdatePlayerHealth(Health);
    }

    public void HealthManager()
    {
        if (Health < 1)
        {
            Dead = true;
            rb.velocity = new Vector2(0f, rb.velocity.y);
            playerAnimation.Dying();
            audi.clip = deathAudio;
            audi.Play();
            UIManager.Instance.GameOverPanelActivation();
        }
    }

    public void IncreaseGemCount(int amount)
    {
        Gems += amount;
        UIManager.Instance.UpdatePlayerGemCount(Gems);
    }
}
