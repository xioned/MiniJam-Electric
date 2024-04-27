using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurretProjectile : MonoBehaviour
{
    public int damageAmount;
    public GameObject projectileDestroyParticle;
    public UnityEvent onDestroyEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Health>())
        {
            collision.GetComponent<Health>().Decreasehealth(damageAmount);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        onDestroyEvent?.Invoke();
    }
}
