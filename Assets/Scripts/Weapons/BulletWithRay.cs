using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWithRay : Bullet {
    [SerializeField]
    private LineRenderer _lineRenderer;

    private Vector3 _startPoint;
    public float LineDissapearTime = 0.3f;

    public override void ShootMe(Vector3 dir) {
        base.ShootMe(dir);
        _startPoint = transform.position;
        _lineRenderer.gameObject.transform.SetParent(ShooterController.BulletsHolder);
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _startPoint);
    }

    private void OnDestroy() {
        Destroy(_lineRenderer.gameObject, LineDissapearTime);
    }
}