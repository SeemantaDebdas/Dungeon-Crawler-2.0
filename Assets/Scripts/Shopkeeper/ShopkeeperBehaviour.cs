using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperBehaviour : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] AudioClip itemButtonSelect;
    [SerializeField] AudioClip purchaseButtonSelect;
    PlayerMovement player;
    AudioSource audi;
    int priceOfSelectedItem;
    int selectedItem;

    private void Start()
    {
        audi = GetComponent<AudioSource>();
        audi.clip = itemButtonSelect;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
            player = other.GetComponent<PlayerMovement>();
            UIManager.Instance.OpenShop(player.Gems);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shopPanel.SetActive(false);
        }
    }

    public void ButtonClicked(int ID)
    {
        audi.clip = itemButtonSelect;
        switch (ID)
        {
            //key to the castle
            case 0:
                UIManager.Instance.UpdateImagePos(280);
                priceOfSelectedItem = 200;
                selectedItem = 0;
                audi.Play();
                break;
            //boots of flight
            case 1:
                UIManager.Instance.UpdateImagePos(148);
                priceOfSelectedItem = 300;
                selectedItem = 1;
                audi.Play();
                break;
            //flame sword
            case 2:
                UIManager.Instance.UpdateImagePos(11);
                priceOfSelectedItem = 400;
                selectedItem = 2;
                audi.Play();
                break;
        }
    }

    public void Buy()
    {
        if (player.Gems >= priceOfSelectedItem)
        {
            UIManager.Instance.DisableOnPurchase(selectedItem);
            player.Gems -= priceOfSelectedItem;
            if (selectedItem == 0)
                GameManager.Instance.HasCastleKey = true;


            audi.clip = purchaseButtonSelect;
            audi.Play();

        }
        else
        {
            Debug.Log("You don't Have Enough Gems To Buy This Item.");
        }
    }
}
