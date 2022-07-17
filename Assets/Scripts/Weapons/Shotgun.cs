using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase {
    
    public float directionRandomness = 0.25f;
    public float bulletsInPackAmount = 5;

    protected override void ShootAbility(Ray ray) {
        base.ShootAbility(ray);
        for (int i = 0; i < bulletsInPackAmount; i++) {
            Bullet shootedBullet = Instantiate(Bullet, ray.origin, Quaternion.LookRotation(ray.direction), ShooterController.BulletsHolder);
            shootedBullet.ShootMe(ray.direction + Random.insideUnitSphere.normalized * directionRandomness);
        }
    }
}