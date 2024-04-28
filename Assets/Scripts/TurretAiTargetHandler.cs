using System.Collections.Generic;
using UnityEngine;

public class TurretAiTargetHandler : MonoBehaviour
{
    public Enemy currentTargetedEnemy = null;
    public List<Enemy> enemiesInShootRange = new();
    public bool canTarget = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTarget) { return; }
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        enemiesInShootRange.Add(enemy);
        enemy.targetedByTurrets.Add(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemiesInShootRange.Count == 0) { return; }
        if (!collision.GetComponent<Enemy>()) { return; }
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!enemiesInShootRange.Contains(enemy)) { return; }
        enemiesInShootRange.Remove(enemy);
    }
}
