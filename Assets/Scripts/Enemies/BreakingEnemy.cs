using UnityEngine;
using Random = UnityEngine.Random;

public class BreakingEnemy : Enemy {
    
    [Header("Breaking")]
    public int EnemiesAmount;
    public Enemy Enemy;

    public override void Die() {
        base.Die();
        for (int i = 0; i < EnemiesAmount; i++) {
            Enemy enemy =  Instantiate(Enemy, transform.position + (Vector3.up*1f + Random.insideUnitSphere).normalized, Random.rotation);
            enemy.AddImpulse(Vector3.up * 10);
            enemy.isAwake = true;
        }
    }
}