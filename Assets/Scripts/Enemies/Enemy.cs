using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float DamageRecall = 0.3f;

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
    }

    protected virtual void Move() {
        navAgent.destination = PlayerController.BodyPosition;
    }

    public virtual void AddImpulse(Vector3 impulse) {
        grounded = false;
        SwitchToRigidBody();
        Rigidbody.AddForce(impulse, ForceMode.Impulse);
        //StartCoroutine(GetDamage(impulse.magnitude));
    }

   /* protected virtual IEnumerator GetDamage(float recallTime) {
        navAgent.enabled = false;
        yield return new WaitForSeconds(recallTime / 100);
        navAgent.enabled = true;
    }*/

    protected void SwitchToRigidBody() {
        navAgent.enabled = false;
        /*if (navAgent.enabled)
        {
            // set the agents target to where you are before the jump
            // this stops her before she jumps. Alternatively, you could
            // cache this value, and set it again once the jump is complete
            // to continue the original move
            //navAgent.SetDestination(PlayerController.BodyPosition);
            // disable the agent
            navAgent.updatePosition = false;
            navAgent.updateRotation = false;
            navAgent.isStopped = true;
        }*/
        // make the jump
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
    }

    protected void SwitchToNavMesh() {
        navAgent.enabled = true;
        /*if (navAgent.enabled) {
            Vector3 pos = transform.position;
            navAgent.updatePosition = true;
            navAgent.updateRotation = true;
            navAgent.isStopped = false;
            navAgent.Warp(pos);
        }*/
        Rigidbody.isKinematic = true;
        Rigidbody.useGravity = false;
       
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.collider.tag == "Ground")
        {
            if (!grounded) {
                SwitchToNavMesh();
                grounded = true;
            }
        }
    }
}