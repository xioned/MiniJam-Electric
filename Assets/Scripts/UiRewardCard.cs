using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiRewardCard : MonoBehaviour, IPointerClickHandler
{
    public RewardManager rewardManager;
    public Reward reward;
    public Image cardImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI quantity;
    public TextMeshProUGUI description;
    public void OnPointerClick(PointerEventData eventData)
    {
        rewardManager.SetPlaceableObject(reward,this);
    }

    public void SetCardDerails(Reward reward)
    {
        this.reward = reward;
        cardImage.sprite = reward.cardSprite;
        itemName.text = reward.name;
        quantity.text = "x"+reward.quantity.ToString();
        description.text = reward.description;
    }
}
