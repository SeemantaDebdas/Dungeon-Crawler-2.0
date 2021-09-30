using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gemValue = 5;
    [SerializeField] AudioClip DiamondClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.IncreaseGemCount(gemValue);
            AudioSource.PlayClipAtPoint(DiamondClip,Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
