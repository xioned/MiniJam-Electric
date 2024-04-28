using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    public int generatePowerAmount = 20;
    public AudioClip turnOnAudio;
    void Start()
    {
        PowerManager.Singleton.AddPowerGenerator(this);
        if (turnOnAudio) { AudioManager.PlaySFX(turnOnAudio); }
    }

    private void OnDestroy()
    {
        PowerManager.Singleton.generatedPowerAmount -= generatePowerAmount;
        PowerManager.Singleton.powerGeneratorList.Remove(this);
    }
}
