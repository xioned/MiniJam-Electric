using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretAI : MonoBehaviour
{
    [Header("Aim")]
    public float turretRotationSpeed = 2f;
    public bool targetTheNearestFirst = false;
    public Transform barrel;
    public Enemy currentTargetedEnemy = null;
    public List<Enemy> enemiesInShootRange = new();
    [Header("Shoot")]
    public float fireRate;
    public Transform projectileSpawnPos;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public AudioClip shootAudio;
    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (currentTargetedEnemy)
        {
            ShootAtCurrentRargetedEnemy();
            return;
        }
        if(enemiesInShootRange.Count == 0) { return; }
        currentTargetedEnemy = enemiesInShootRange[0];
    }

    private void ShootAtCurrentRargetedEnemy()
    {
        Vector3 shootDirection = currentTargetedEnemy.transform.position - barrel.position;
        float targetAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        barrel.rotation = Quaternion.Lerp(barrel.rotation, Quaternion.Euler(0, 0, targetAngle), turretRotationSpeed * Time.deltaTime);
        Debug.Log(targetAngle + ":" + barrel.rotation.eulerAngles.z);
        if (timer >= fireRate)
        {
            if (Vector3.Cross(shootDirection,projectileSpawnPos.position).z < 0.01f)
            {
                projectileSpawnPos.GetChild(0).gameObject.SetActive(true);
                AudioManager.PlaySFX(shootAudio);
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPos.position, projectileSpawnPos.rotation);
                Vector2 direction = (currentTargetedEnemy.transform.position - projectileSpawnPos.position).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        enemiesInShootRange.Add(enemy);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        if(!enemiesInShootRange.Contains(enemy)) { return; }
        enemiesInShootRange.Remove(enemy);
    }
}
