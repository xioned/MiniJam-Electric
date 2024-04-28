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
    public GameObject projectile;
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
            projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
            StartCoroutine(ShootProjectileRoutine(projectile));
            timer = 0;
        }
    }
    IEnumerator ShootProjectileRoutine(GameObject projectile)
    {
        AudioManager.PlaySFX(shootAudio);
        while (projectile.transform.childCount > 0)
        {
            Transform projectileChild = projectile.transform.GetChild(0);
            projectileChild.SetParent(null);
            projectileChild.position = projectileSpawnPos.position;
            projectileChild.gameObject.SetActive(true);
            projectileChild.GetComponent<Rigidbody2D>().velocity = barrel.transform.right * projectileSpeed;
            yield return new WaitForSeconds(0.12f);
        }
        projectileSpawnPos.GetChild(0).gameObject.SetActive(true);
        Destroy(projectile);
    }
}
