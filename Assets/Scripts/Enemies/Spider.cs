using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamagable
{

    [SerializeField] GameObject acidPrefab;

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = health;
    }

    public void Damage()
    {
        Health--;
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
    }

    public override void Movement()
    {
        //stay still
        
    }

    public void Attack()
    {
        Instantiate(acidPrefab, transform.position, Quaternion.identity);
        audi.clip = enemyAttack;
        audi.Play();
    }
}
