using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBehaviour : MonoBehaviour
{
    float speed = 3f;
    private void Start()
    {
        Destroy(gameObject, 5f);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamagable hit = other.GetComponent<IDamagable>();
            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}
