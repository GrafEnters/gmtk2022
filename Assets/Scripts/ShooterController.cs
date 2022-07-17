using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {
    [SerializeField]
    private Transform _bulletsHolder;

    [SerializeField]
    private Transform _shootPoint;

    public static Transform BulletsHolder;

    public float maxTargetDistance = 100;

    [SerializeField]
    protected LayerMask shootMask;

    [SerializeField]
    private WeaponBase[] weapons;

    public WeaponBase curWeapon;
    public Transform TargetTransform;
    [SerializeField]
    private LineRenderer _lineRenderer;

    public bool canChangeByNumbers = false;
    public bool lockShooting = false;
    private void Awake() {
        BulletsHolder = _bulletsHolder;
    }

    void Update() {
        if(lockShooting)
            return;
        if (Input.GetMouseButton(0)) {
            Vector3 target;
            Ray raycastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(raycastRay, out RaycastHit info, maxTargetDistance, shootMask)) {
                target = info.point;
            } else {
                target = raycastRay.GetPoint(maxTargetDistance);
            }

            TargetTransform.position = target;
            TargetTransform.forward = raycastRay.direction;

            Ray shootRay = new(_shootPoint.position, TargetTransform.position - _shootPoint.position);
           
            //DrawLine(shootRay);
            
            curWeapon.Shoot(shootRay);
        }

        if (canChangeByNumbers) {
            if (Input.GetKeyDown(KeyCode.Alpha0)) ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(2);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(3);
            if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(4);
            if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeWeapon(5);
        }
     
    }

    public void ChangeWeapon(int weaponIndex) {
        curWeapon = weapons[weaponIndex];
        curWeapon.Equip();
        UIManager.Instance.ChangeWeapon(weaponIndex);
    }

    private void DrawLine(Ray ray) {
        _lineRenderer.transform.position = ray.origin;
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, ray.origin);
        _lineRenderer.SetPosition(1, ray.GetPoint(100));
    }
}