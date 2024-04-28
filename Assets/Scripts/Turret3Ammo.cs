using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret3Ammo : MonoBehaviour
{
    public LayerMask mask;
    public TurretAI_3 turretAI;
    private void OnEnable()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(11, 0.3f), transform.rotation.eulerAngles.z, mask);
        turretAI.DamageEnemy(colliders);
    }
}
