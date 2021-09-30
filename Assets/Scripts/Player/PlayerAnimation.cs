using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    //cache reference
    Animator anim;
    Animator swordArc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        swordArc = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float moveParameter)
    {
        anim.SetFloat("Move", Mathf.Abs(moveParameter));
    }

    public void Jumping(bool jumpParameter)
    {
        anim.SetBool("Jumping", jumpParameter);
    }

    public void Attacking()
    {
        anim.SetTrigger("Attacking");
        swordArc.SetTrigger("SwordAnimation");
    }

    public void Dying()
    {
        anim.SetTrigger("DeathTrigger");
    }
}
