using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Bullet {

   [SerializeField]
   private GameObject explosion;
   public override void ShootMe(Vector3 dir) {
      Destroy(gameObject, DestroyTime);
   }

   protected override void FixedUpdate() {
   }

   protected override void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.TryGetComponent(out Enemy enemy)) {
         Debug.Log("Hitted enemy");
         enemy.AddImpulse((enemy.transform.position - transform.position).normalized * knockbackForce);
         enemy.TakeDamage(Damage);
         GameObject ex = Instantiate(explosion, transform.position, Quaternion.identity);
         Destroy(ex, 2f);
         Destroy(gameObject);
      }
   }
}