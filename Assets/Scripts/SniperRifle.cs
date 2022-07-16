using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : WeaponBase {
    [SerializeField]
    private LayerMask enemyLayer;

    public float maxDistance = 50;
    public float impulsePower = 50;

    protected override void ShootAbility(Ray ray) {
        base.ShootAbility(ray);

        if (Physics.Raycast(ray, out RaycastHit info, maxDistance, enemyLayer)) {
            if (info.transform.gameObject.TryGetComponent(out Enemy enemyComponent)) {
                enemyComponent.AddImpulse(ray.direction * impulsePower);
            }
        }

        Bullet shootedBullet = Instantiate(Bullet, ray.origin, Quaternion.LookRotation(ray.direction),
            ShooterController.BulletsHolder);
        shootedBullet.ShootMe(ray.direction);
    }
}