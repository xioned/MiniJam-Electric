using System.Collections;
using UnityEngine;
[RequireComponent(typeof(TurretAiTargetHandler))]
public class TurretAI_2 : MonoBehaviour
{
    [Header("Aim")]
    public float turretRotationSpeed = 2f;
    public bool targetTheNearestFirst = false;
    public Transform barrel;
    TurretAiTargetHandler targetHandler;
    [Header("Shoot")]
    public float fireRate;
    public Transform projectileSpawnPos;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public AudioClip shootAudio;
    float timer;
    [Header("Power")]
    public int powerCostingAmount;
    Vector3 shootDirection;
    private void Start()
    {
        targetHandler = GetComponent<TurretAiTargetHandler>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (targetHandler.currentTargetedEnemy) { RotateBarrel(); }
        if(targetHandler.enemiesInShootRange.Count == 0) { return; }
        targetHandler.currentTargetedEnemy = targetHandler.enemiesInShootRange[0];
    }

    private void RotateBarrel()
    {
        shootDirection = targetHandler.currentTargetedEnemy.transform.position - barrel.position;
        float targetAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        barrel.rotation = Quaternion.Lerp(barrel.rotation, Quaternion.Euler(0, 0, targetAngle), turretRotationSpeed * Time.deltaTime);
        FireProjectile();
    }
    private void FireProjectile()
    {
        if (timer > fireRate)
        {
            if (Vector3.Cross(shootDirection, projectileSpawnPos.position).z > 0.01f) { return; }
            AudioManager.PlaySFX(shootAudio);
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
            Vector2 direction = (targetHandler.currentTargetedEnemy.transform.position - projectileSpawnPos.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            projectileSpawnPos.GetChild(0).gameObject.SetActive(true);
            timer = 0;
        }
    }
}
