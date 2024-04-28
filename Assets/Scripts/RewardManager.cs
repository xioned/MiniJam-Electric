using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public Reward[] turretRewards;
    public Reward[] powerGeneratorRewards;
    public UiRewardCard[] uiRewardCards;
    public ObjectPlaceManager placeManager;
    private void Start()
    {
        CreateReward();
    }
    public void CreateReward()
    {
        CreatePowerGeneratorReward();
        CreateTurretReward();
        gameObject.SetActive(true);
        for (int i = 0; i < uiRewardCards.Length; i++)
        {
            uiRewardCards[i].gameObject.SetActive(true);
        }
    }

    private void CreatePowerGeneratorReward()
    {
        int randReward = Random.Range(0, powerGeneratorRewards.Length);
        uiRewardCards[1].SetCardDerails(powerGeneratorRewards[randReward]);
    }
    private void CreateTurretReward()
    {
        int randReward = Random.Range(0, turretRewards.Length);
        uiRewardCards[0].SetCardDerails(turretRewards[randReward]);
    }

    public void SetPlaceableObject(Reward rewards,UiRewardCard rewardCard)
    {
        placeManager.SetPlaceableObject(rewards);
        gameObject.SetActive(false);
    }

    
}

[System.Serializable]
public struct Reward
{
    public string name;
    public int id;
    public int quantity;
    public Sprite cardSprite;
    [TextArea] 
    public string description;
}
