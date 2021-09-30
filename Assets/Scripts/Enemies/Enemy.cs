using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected GameObject diamondPrefab;

    [Header("AudioClips")]
    [SerializeField] protected AudioClip enemyWalk;
    [SerializeField] protected AudioClip enemyHit;
    [SerializeField] protected AudioClip enemyAttack;
    [SerializeField] protected AudioClip enemyDeath;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected PlayerMovement player;
    protected BoxCollider2D boxCollider2D;
    protected AudioSource audi;

    protected bool isHit = false;
    protected bool isDead = false;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        audi = GetComponent<AudioSource>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("CombatBool")==false)
        {
            return;
        }

        if (!isDead)
            Movement();
        else if (isDead)
            boxCollider2D.enabled = false;
    }
    public virtual void Movement()
    {
        FlipSprite();
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("IdleTrigger");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("IdleTrigger");
        }

        if(!isHit)
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        DistanceCheck();
    }

    void FlipSprite()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }
    }

    void DistanceCheck()
    {
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > 2f)
        {
            isHit = false;
            anim.SetBool("CombatBool", false);
        }

        Vector3 direction = transform.localPosition - player.transform.localPosition;
        if (anim.GetBool("CombatBool") == true)
        {
            if (direction.x > 0)
            {
                sprite.flipX = true;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = false;
            }
        }
    }
}
