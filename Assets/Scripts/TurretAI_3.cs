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
