using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Bullet
{
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private GameObject explosion;

    public float SphereRadius = 2;
    
    protected override void OnCollisionEnter(Collision collision) {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, SphereRadius, enemyLayer);
        foreach (Collider enemyCollider in colliders) {
            if(enemyCollider.attachedRigidbody. gameObject.TryGetComponent(out Enemy enemy)) {
                enemy.AddImpulse((enemy.transform.position - transform.position).normalized * knockbackForce);
                enemy.TakeDamage(Damage);
            }
        }
        GameObject ex = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(ex, 2f);
        Destroy(gameObject);
    }
}
