using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public int generatedPowerAmount = 0;
    public int consumingPowerAmount = 0;

    public static PowerManager Singleton;
    private void Awake()
    {
        Singleton = this;
    }
    public bool ConsumePower(int amount)
    {
        int newConsumingPowerAmount = amount + consumingPowerAmount;
        if (consumingPowerAmount < newConsumingPowerAmount)
        {
            return false;
        }
        else
        {
            consumingPowerAmount+=amount;
            return true;
        }
    }

    public void AddGeneratedPower(int amount)
    {
        generatedPowerAmount += amount;
    }
}
