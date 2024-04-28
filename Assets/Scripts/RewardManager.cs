using System.Collections;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    
    public UiRewardCard[] rewardCards;
    public ObjectPlaceManager placeManager;
    private void Start()
    {
        CreateReward();
    }
    public void CreateReward()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < rewardCards.Length; i++)
        {
            rewardCards[i].gameObject.SetActive(true);
        }
    }

    public void SetPlaceableObject(Reward rewards,UiRewardCard rewardCard)
    {
        placeManager.SetPlaceableObject(rewards);
        gameObject.SetActive(false);
    }
}
