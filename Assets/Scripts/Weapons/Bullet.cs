using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletSpeed = 25;
    public float DestroyTime = 10;
    public float knockbackForce = 3f;

    private Vector3 direction;
    private bool isEnemy;

    public virtual void ShootMe(Vector3 dir) {
        direction = dir;
        Destroy(gameObject, DestroyTime);
    }

    protected virtual void FixedUpdate() {
        transform.position += direction * (bulletSpeed * Time.fixedDeltaTime);
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        if (isEnemy && collision.gameObject.TryGetComponent(out PlayerController playerController)) {
            playerController.TakeDamage(collision.contacts[0].normal * -1);
        }
        else if (collision.gameObject.TryGetComponent(out Enemy enemy)) {
            Debug.Log("Hitted enemy");
            enemy.AddImpulse(transform.forward * bulletSpeed * knockbackForce);
        }

        Destroy(gameObject);
    }
}