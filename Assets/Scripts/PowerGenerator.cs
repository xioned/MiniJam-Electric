using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    public int generatePowerAmount = 20;
    public AudioClip turnOnAudio;
    void Start()
    {
        AudioManager.PlaySFX(turnOnAudio);
        PowerManager.Singleton.AddGeneratedPower(generatePowerAmount);
    }
}
