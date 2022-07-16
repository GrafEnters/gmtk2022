using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : Enemy {
    public float Jumpheight;
    public float JumpDistance;
    
    
  protected override void Move() {
      if (grounded) {
          Jump();
      }
  }

  private void Jump() {
      Vector3 dir = (PlayerController.BodyPosition - transform.position).normalized * JumpDistance + Vector3.up * Jumpheight;
      AddImpulse(dir);
      //Rigidbody.AddRelativeForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
  }
}
