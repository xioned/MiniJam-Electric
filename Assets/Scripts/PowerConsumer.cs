using UnityEngine;
using UnityEngine.Events;

public class PowerConsumer : MonoBehaviour
{
    public int powerConsumeAmount;
    public UnityEvent OnPowerConsume;
    private void Start()
    {
        if (PowerManager.Singleton.AddPowerConsumer(this))
        {
            OnPowerConsume?.Invoke();
        }
    }
    public void StartConsumingPower()
    {
        OnPowerConsume?.Invoke();
    }
}
