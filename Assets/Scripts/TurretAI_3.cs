using System.Collections;
using UnityEngine;
[RequireComponent(typeof(TurretAiTargetHandler))]
public class TurretAI_3 : MonoBehaviour
{
    [Header("Aim")]
    public float turretRotationSpeed = 2f;
    public bool targetTheNearestFirst = false;
    public Transform barrel;
    TurretAiTargetHandler targetHandler;
    [Header("Shoot")]
    public float fireRate;
    public Transform projectileSpawnPos;
    public GameObject beamObject;
    public float projectileSpeed;
    public AudioClip shootAudio;
    float timer;
    [Header("Power")]
    public int powerCostingAmount;
    Vector3 shootDirection;
    public int damageAmount;
    private void Start()
    {
        targetHandler = GetComponent<TurretAiTargetHandler>();
        timer = fireRate;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (beamObject.activeSelf) { return; }
        if (targetHandler.currentTargetedEnemy) { RotateBarrel(); }
        if(targetHandler.enemiesInShootRange.Count == 0) { return; }
        targetHandler.currentTargetedEnemy = targetHandler.enemiesInShootRange[0];
    }
    public void DamageEnemy(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Health>().Decreasehealth(damageAmount);
        }
    }
    private void RotateBarrel()
    {
        shootDirection = targetHandler.currentTargetedEnemy.transform.position - barrel.position;
        float targetAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        barrel.rotation = Quaternion.Lerp(barrel.rotation, Quaternion.Euler(0, 0, targetAngle), turretRotationSpeed * Time.deltaTime);
        if (Vector3.Cross(shootDirection, projectileSpawnPos.position).z > 0.01f) { return; }
        FireProjectile();
    }
    private void FireProjectile()
    {
        if (timer > fireRate)
        {
            AudioManager.PlaySFX(shootAudio);
            StartCoroutine(BeamRoutine());
            timer = 0;
        }
    }

    IEnumerator BeamRoutine()
    {
        beamObject.SetActive(true);
        yield return new WaitForSeconds(.5f);
        beamObject.SetActive(false);
    }
}
