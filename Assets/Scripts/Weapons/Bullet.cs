using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 25;
    public float DestroyTime = 10;
    public float knockbackForce = 3f;
    public int Damage = 1;
    public bool isEnemy;
    
    private Vector3 _direction;
  

    public virtual void ShootMe(Vector3 dir) {
        _direction = dir;
        Destroy(gameObject, DestroyTime);
    }

    protected virtual void FixedUpdate() {
        transform.position += _direction * (bulletSpeed * Time.fixedDeltaTime);
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        if (isEnemy && collision.gameObject.TryGetComponent(out PlayerController playerController)) {
            playerController.TakeDamage(collision.contacts[0].normal * -1);
        } else if (collision.gameObject.TryGetComponent(out Enemy enemy)) {
            Debug.Log("Hitted enemy");
            enemy.AddImpulse(transform.forward * bulletSpeed * knockbackForce);
            enemy.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}