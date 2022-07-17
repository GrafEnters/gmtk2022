using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : WeaponBase {
    [SerializeField]
    private LayerMask enemyLayer;

    public float maxDistance = 50;
    public float impulsePower = 50;

    protected override void ShootAbility(Ray ray) {
        base.ShootAbility(ray);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2)), out RaycastHit info, maxDistance, enemyLayer)) {
            if (info.transform.gameObject.TryGetComponent(out Enemy enemyComponent)) {
                enemyComponent.AddImpulse((ray.direction * -1 + Vector3.up).normalized * impulsePower);
                enemyComponent.AddTorque((ray.direction * -1 + Vector3.up).normalized * impulsePower);
            }
        }
    }
    
    
}
