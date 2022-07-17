using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float DamageRecall = 0.3f;
    public float hp = 3;
    public float DeaggroDistance = 40f;
    public float knockBack = 4;

    [SerializeField]
    protected NavMeshAgent navAgent;

    [SerializeField]
    protected bool isAwake = false;

    [SerializeField]
    protected Rigidbody Rigidbody;

    public bool grounded = true;

    public void Init() {
        isAwake = true;
    }

    private void Start() {
        SwitchToNavMesh();
    }

    protected virtual void Update() {
        if (isAwake && navAgent.enabled) {
            Move();
        }

        if (Vector3.Magnitude(transform.position - PlayerController.BodyPosition) > DeaggroDistance) {
            isAwake = false;
        }
    }

    protected virtual void Move() {
        navAgent.destination = PlayerController.BodyPosition;
    }

    public virtual void AddImpulse(Vector3 impulse) {
        grounded = false;
        SwitchToRigidBody();
        Rigidbody.AddForce(impulse, ForceMode.Impulse);
    }

    protected void SwitchToRigidBody() {
        navAgent.enabled = false;
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
    }

    protected virtual void SwitchToNavMesh() {
        navAgent.enabled = true;
        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;
    }

    public void TakeDamage(int amount) {
        hp -= amount;
        if (hp <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider != null && collision.collider.CompareTag("Ground")) {
            if (!grounded) {
                SwitchToNavMesh();
                grounded = true;
            }
        }

        if (collision.collider != null && collision.collider.CompareTag("Player")) {
            Vector3 knockbackVector = collision.transform.position - transform.position;
            knockbackVector.y = 0;
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(knockbackVector.normalized * 7);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            isAwake = true;
        }
    }

    /* private void OnTriggerExit(Collider other) {
         if (other.gameObject.CompareTag("Player")) {
             isAwake = false;
         }
     }*/
}