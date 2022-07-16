using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : WeaponBase
{
   protected override void ShootAbility(Ray ray) {
      Instantiate(Bullet, ray.origin, Quaternion.LookRotation(ray.direction), ShooterController.BulletsHolder);
   }
}
