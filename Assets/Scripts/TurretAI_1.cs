using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(TurretAiTargetHandler))]
public class TurretAI_1 : MonoBehaviour
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
        if(targetHandler.currentTargetedEnemy) { RotateBarrel(); }
        if(targetHandler.enemiesInShootRange.Count == 0) { return; }
        targetHandler.currentTargetedEnemy = targetHandler.enemiesInShootRange[0];
    }

    private void RotateBarrel()
    {
        Vector3 direction = targetHandler.currentTargetedEnemy.transform.position - barrel.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        barrel.rotation = Quaternion.RotateTowards(barrel.transform.rotation, targetRotation, turretRotationSpeed * Time.deltaTime);
        if (Vector3.Angle(barrel.transform.right, direction) < 0.1f)
        {
            FireProjectile();
        }
    }
    private void FireProjectile()
    {
        if (timer > fireRate)
        {
            if (Vector3.Cross(shootDirection, projectileSpawnPos.position).z > 0.02f) { return; }
            AudioManager.PlaySFX(shootAudio);
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
            Vector2 direction = (targetHandler.currentTargetedEnemy.transform.position - projectileSpawnPos.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            projectileSpawnPos.GetChild(0).gameObject.SetActive(true);
            timer = 0;
        }
    }
}
