using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public float DamageRecall = 0.3f;

    [SerializeField]
    private NavMeshAgent navAgent;

    [SerializeField]
    private bool isAwake = false;

    [SerializeField]
    private Rigidbody Rigidbody;

    public void Init() {
        isAwake = true;
    }

    private void Update() {
        if (isAwake && navAgent.enabled) {
            navAgent.destination = PlayerController.BodyPosition;
        }
    }

    public void AddImpulse(Vector3 impulse) {
        Rigidbody.AddForce(impulse, ForceMode.Impulse);
        StartCoroutine(GetDamage(impulse.magnitude));
    }

    private IEnumerator GetDamage(float recallTime) {
        navAgent.enabled = false;
        yield return new WaitForSeconds(recallTime / 100);
        navAgent.enabled = true;
    }
}