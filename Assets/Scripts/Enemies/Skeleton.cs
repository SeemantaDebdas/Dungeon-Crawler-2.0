using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public override void Movement()
    {
        base.Movement();
    }


    public void Damage()
    {
        isHit = true;
        Health--;

        audi.clip = enemyHit;
        audi.Play();

        if (Health < 1)
        {
            anim.SetTrigger("DeathTrigger");

            audi.Stop();
            audi.clip = enemyDeath;
            audi.Play();

            isDead = true;
            GameObject diamondSpawn = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamondSpawn.GetComponent<Diamond>().gemValue = gems;
        }
        anim.SetBool("CombatBool", true);
        anim.SetTrigger("HitTrigger");
    }
}
