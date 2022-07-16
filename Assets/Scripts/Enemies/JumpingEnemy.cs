using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy {
    public float Jumpheight;
    public float JumpDistance;
    public float JumpTime = 0.7f;
    private bool isJumping;
    private bool lockNoNavMesh;
  protected override void Move() {
      if (grounded && !isJumping) {
          Jump();
      }
  }

  private void Jump() {
      isJumping = true;
      Vector3 dir = (PlayerController.BodyPosition - transform.position).normalized * JumpDistance + Vector3.up * Jumpheight;
      AddImpulse(dir);
      StartCoroutine(JumpCoroutine());
      //Rigidbody.AddRelativeForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
  }

  private IEnumerator JumpCoroutine() {
      lockNoNavMesh = true;
      yield return new WaitForSeconds(JumpTime);
      lockNoNavMesh = false;
      if(grounded)
          SwitchToNavMesh();
  }

  protected override void SwitchToNavMesh() {
      if(lockNoNavMesh)
          return;;
      base.SwitchToNavMesh();
      isJumping = false;
  }
}
