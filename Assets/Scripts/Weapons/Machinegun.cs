using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : WeaponBase
{
    public float directionRandomness = 0.25f;

    protected override void ShootAbility(Ray ray) {
        base.ShootAbility(ray);
        Bullet shootedBullet = Instantiate(Bullet, ray.origin, Quaternion.LookRotation(ray.direction),
            ShooterController.BulletsHolder);
        shootedBullet.ShootMe(ray.direction + Random.insideUnitSphere.normalized * directionRandomness);
    }
}
