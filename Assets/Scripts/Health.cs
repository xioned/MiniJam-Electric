using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startingHealthAmount;
    public int currentHealthamount;
    public UnityEvent OnHealthZero;
    private void Start()
    {
        currentHealthamount = startingHealthAmount;
    }
    public void Decreasehealth(int amount)
    {
        currentHealthamount -= amount;
        if (currentHealthamount > 0) { return; }
        OnHealthZero?.Invoke();
    }
}
