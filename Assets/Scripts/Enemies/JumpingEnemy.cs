using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy {
    [Header("Jumping")]
    public float JumpHeight;

    public float JumpDistance;
    public float JumpTime = 0.7f;
    private bool _isJumping;
    private bool _lockNoNavMesh;

    protected override void Move() {
        if (grounded && !_isJumping) {
            Jump();
        }
    }

    private void Jump() {
        _isJumping = true;
        Vector3 dir = (PlayerController.BodyPosition - transform.position).normalized * JumpDistance +
                      Vector3.up * JumpHeight;
        Vector3 noY = (PlayerController.BodyPosition - transform.position);
        noY.y = 0;
        transform.rotation = Quaternion.LookRotation(noY, Vector3.up);
        AddImpulse(dir);
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine() {
        _lockNoNavMesh = true;
        yield return new WaitForSeconds(JumpTime);
        _lockNoNavMesh = false;
        if (grounded)
            SwitchToNavMesh();
    }

    protected override void SwitchToNavMesh() {
        if (_lockNoNavMesh)
            return;
        ;
        base.SwitchToNavMesh();
        _isJumping = false;
    }
}