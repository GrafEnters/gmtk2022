using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {
    
    [Header("Shooting")]
    public Bullet bullet;
    public Transform shootPoint;

    public float shootDelayTime = 3f;
    public float currentDelay;

    public float directionRandomness = 0.25f;
    public float bulletsInPackAmount = 5;

    protected override void Update() {
        base.Update();
        if(isAwake) {
            if (currentDelay > shootDelayTime) {
                currentDelay = 0;
                Shoot();
            } else {
                currentDelay += Time.deltaTime;
            }
        }
    }

    private void Shoot() {
        Ray ray = new Ray(shootPoint.position, PlayerController.BodyPosition - shootPoint.position);
        for (int i = 0; i < bulletsInPackAmount; i++) {
            Bullet shootedBullet = Instantiate(bullet, ray.origin, Quaternion.LookRotation(ray.direction),
                ShooterController.BulletsHolder);
            shootedBullet.ShootMe(ray.direction + Random.insideUnitSphere.normalized * directionRandomness);
        }
    }
}