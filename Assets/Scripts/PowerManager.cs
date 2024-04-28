using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public int InitialPower = 30;
    public int generatedPowerAmount = 0;
    public int consumingPowerAmount = 0;
    public TextMeshProUGUI powerText;
    public List<PowerConsumer> powerConsumerList = new();
    public List<PowerGenerator> powerGeneratorList = new();
    public List<PowerConsumer> watingForPowerList = new();

    public static PowerManager Singleton;
    private void Awake()
    {
        Singleton = this;
    }
    private void Start()
    {
        generatedPowerAmount = 30;
        powerText.text = consumingPowerAmount.ToString() + "/" + generatedPowerAmount.ToString();
    }
    public bool AddPowerConsumer(PowerConsumer powerConsumer)
    {
        int newConsumingPowerAmount = powerConsumer.powerConsumeAmount + consumingPowerAmount;
        if (generatedPowerAmount < newConsumingPowerAmount)
        {
            watingForPowerList.Add(powerConsumer);
            return false;
        }
        else
        {
            powerConsumerList.Add(powerConsumer);
            consumingPowerAmount+= powerConsumer.powerConsumeAmount;
            powerText.text = consumingPowerAmount.ToString()+"/"+ generatedPowerAmount.ToString();
            return true;
        }
    }

    public void AddPowerGenerator(PowerGenerator powerGenerator)
    {
        powerGeneratorList.Add(powerGenerator);
        generatedPowerAmount += powerGenerator.generatePowerAmount;
        TryFeedingWatingForPowerConsumer();
        powerText.text = consumingPowerAmount.ToString() + "/" + generatedPowerAmount.ToString();
    }
    private void TryFeedingWatingForPowerConsumer()
    {
        for (int i = 0; i < watingForPowerList.Count; i++)
        {
            int needPowerToRun = watingForPowerList[i].powerConsumeAmount + consumingPowerAmount;
            if (needPowerToRun > generatedPowerAmount)
            {
                continue;
            }
            consumingPowerAmount += watingForPowerList[i].powerConsumeAmount;
            watingForPowerList[i].StartConsumingPower();
            watingForPowerList.RemoveAt(i);
        }
    }
    public void RemovePowerGenerator(PowerGenerator powerGenerator)
    {
        powerGeneratorList.Remove(powerGenerator);
        generatedPowerAmount -= powerGenerator.generatePowerAmount;
        powerText.text = consumingPowerAmount.ToString() + "/" + generatedPowerAmount.ToString();
    }
    public void RemovePowerConsumer(PowerConsumer powerConsumer)
    {
        powerConsumerList.Remove(powerConsumer);
        consumingPowerAmount += powerConsumer.powerConsumeAmount;
        powerText.text = consumingPowerAmount.ToString() + "/" + generatedPowerAmount.ToString();
    }
}
