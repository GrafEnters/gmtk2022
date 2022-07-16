using UnityEngine;
using Random = UnityEngine.Random;

public class BreakingEnemy : Enemy {
    
    [Header("Breaking")]
    public int EnemiesAmount;
    public GameObject enemy;

    public override void Die() {
        base.Die();
        for (int i = 0; i < EnemiesAmount; i++) {
            Instantiate(enemy, transform.position + Random.insideUnitSphere, Random.rotation);
        }
    }
}