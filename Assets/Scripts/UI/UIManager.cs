using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                Debug.Log("UIManager not found");
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    [Header("ShopPanel UI")]
    public Text coinsText;
    public Image selectionImage;
    public Button[] shopItemButtons;
    public Text[] shopItemButtonsTexts;

    [Header("Player UI")]
    public Text gemCountText;
    public GameObject[] healthBars;
    public GameObject gameOverPanel;

    public void UpdateImagePos(float yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void OpenShop(int gemCount)
    {
        coinsText.text = gemCount.ToString()+"G";
    }

    public void DisableOnPurchase(int ID)
    {
        shopItemButtons[ID].interactable = false;
        shopItemButtonsTexts[ID].color = Color.green;
    }

    public void UpdatePlayerGemCount(int amount)
    {
        gemCountText.text = amount.ToString() + "G";
    }

    public void UpdatePlayerHealth(int healthCount)
    {
        int i = 0;
        while (i < healthCount)
        {
            i++;
        }
        healthBars[i].SetActive(false);
    }

    public void GameOverPanelActivation()
    {
        gameOverPanel.SetActive(true);
    }


}
