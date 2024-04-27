using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public UiRewardCard[] rewardCards;
    public ObjectPlaceManager placeManager;
    public void CreateReward(int wave, int level)
    {
        CreateTurretReward(wave,level);
        CreateDefenceReward(wave, level);
        CreateUtilitiesReward(wave, level);
        CreateFreeReward(wave, level);
    }

    private void CreateFreeReward(int wave, int level)
    {
        throw new NotImplementedException();
    }

    private void CreateUtilitiesReward(int wave, int level)
    {
        throw new NotImplementedException();
    }

    private void CreateDefenceReward(int wave, int level)
    {
        throw new NotImplementedException();
    }

    private void CreateTurretReward(int wave, int level)
    {
        throw new NotImplementedException();
    }

    public void SetPlaceableObject(Reward rewards,UiRewardCard rewardCard)
    {
        for (int i = 0; i < rewardCards.Length; i++)
        {
            if (rewardCards[i] == rewardCard) { continue; }
            rewardCards[i].gameObject.SetActive(false);
        }
        StartCoroutine(SetObjectPlace(rewards, rewardCard));
        
    }
    IEnumerator SetObjectPlace(Reward rewards, UiRewardCard rewardCard)
    {
        yield return new WaitForSeconds(2);
        rewardCard.gameObject.SetActive(false);
        placeManager.SetPlaceableObject(rewards);

    }
}
