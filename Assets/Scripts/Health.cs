using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int startingHealthAmount;
    public int currentHealthamount;
    public UnityEvent OnHealthZero;
    public Material damageMaterial;
    private Material defaultMaterial;
    private void Start()
    {
        currentHealthamount = startingHealthAmount;
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }
    public void Decreasehealth(int amount)
    {
        currentHealthamount -= amount;
        if (currentHealthamount > 0) { return; }
        OnHealthZero?.Invoke();
    }
}
