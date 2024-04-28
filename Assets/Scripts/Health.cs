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
        StartCoroutine(ChangeMaterial());
        if (currentHealthamount > 0) { return; }
        OnHealthZero?.Invoke();
    }

    private IEnumerator ChangeMaterial(){
        GetComponent<SpriteRenderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
}
