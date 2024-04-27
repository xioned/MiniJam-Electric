using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAiTargetHandler : MonoBehaviour
{
    public Enemy currentTargetedEnemy = null;
    public List<Enemy> enemiesInShootRange = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        enemiesInShootRange.Add(enemy);
        enemy.targetedByTurrets.Add(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!enemiesInShootRange.Contains(enemy)) { return; }
        enemiesInShootRange.Remove(enemy);
    }
}
