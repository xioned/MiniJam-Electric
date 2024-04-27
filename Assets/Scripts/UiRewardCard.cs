using UnityEngine;
using UnityEngine.EventSystems;

public class UiRewardCard : MonoBehaviour, IPointerClickHandler
{
    public RewardManager rewardManager;
    public Reward rewards;
    public void OnPointerClick(PointerEventData eventData)
    {
        rewardManager.SetPlaceableObject(rewards,this);
    }
}

public struct Reward
{
    public int id;
    public int quantity;
}