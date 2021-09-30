using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool isActive = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        IDamagable hit = other.GetComponent<IDamagable>();
        if (hit != null)
        {
            if(isActive)
                hit.Damage();
            isActive = false;
            StartCoroutine(HitCooldownTimer());
        }
    }

    IEnumerator HitCooldownTimer()
    {
        yield return new WaitForSeconds(0.7f);
        isActive = true;
    }
}
