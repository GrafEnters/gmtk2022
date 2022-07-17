using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour {
    public float POVNormal = 40, POVAIM = 20;
    public float lerpSpeed = 0.3f;

    public float minDistance = 0.3f;
    public float minRotation = 3f;

    public float PresitionMultiplierInAim = 3;
    public float SensivityLowerInAim = 0.3f;
    public float ClippingFixTries;
    public float ClipSphereRadius = 0.1f;

    public bool isLockAim;
    public LayerMask clipMask;

    [Header("Other")]
    public Transform NormalMode;

    public Transform AimMode;
    private Transform cameraTransform;
    private Camera camera;
    private float targetPov;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    public MouseLook verticalCameraRotation, horizontalCameraRotation;

    private bool isAim;
    private Vector3 clipOffset;

    private void Awake() {
        cameraTransform = Camera.main.transform;
        camera = Camera.main;
    }

    private void Update() {
        if (isLockAim) {
            if (Input.GetMouseButtonDown(1)) {
                isAim = !isAim;
            }
        } else {
            isAim = Input.GetMouseButton(1);
        }
    }
    
    

    void FixedUpdate() {
        Follow();
    }

    private void Follow() {
        if (isAim) {
            targetPov = POVAIM;
            targetPosition = AimMode.position;
            targetRotation = AimMode.rotation;
            verticalCameraRotation.SensivityMultiplier = SensivityLowerInAim;
            horizontalCameraRotation.SensivityMultiplier = SensivityLowerInAim;
        } else {
            targetPov = POVNormal;
            targetPosition = NormalMode.position;
            targetRotation = NormalMode.rotation;
            verticalCameraRotation.SensivityMultiplier = 1;
            horizontalCameraRotation.SensivityMultiplier = 1;
        }

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetPov, lerpSpeed);

        TryFixClip();

        if (minDistance / (isAim ? PresitionMultiplierInAim : 1) <
            Vector3.Distance(cameraTransform.position, targetPosition))
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition + clipOffset,
                lerpSpeed * (isAim ? 2 : 1));
        if (minRotation / (isAim ? PresitionMultiplierInAim : 1) <
            Quaternion.Angle(cameraTransform.rotation, targetRotation))
            cameraTransform.rotation =
                Quaternion.Lerp(cameraTransform.rotation, targetRotation, lerpSpeed * (isAim ? 2 : 1));
    }

    private void TryFixClip() {
        bool isClip = Physics.OverlapSphere(targetPosition, ClipSphereRadius, clipMask).Length > 0;
        if (!isClip) {
            clipOffset = Vector3.zero;
            return;
        }

        for (int i = 0; i < ClippingFixTries; i++) {
            Vector3 dir = PlayerController.BodyPosition - cameraTransform.position;
            targetPosition += dir * 1 / ClippingFixTries;
            isClip = Physics.OverlapSphere(targetPosition, ClipSphereRadius, clipMask).Length > 0;
            if (!isClip)
                return;
        }
        /*clipOffset = Vector3.zero;
         for (int i = 0; i < ClippingFixTries; i++) {
             Vector3 dir = PlayerController.BodyPosition - cameraTransform.position;
             clipOffset += dir * -1;
             isClip = Physics.OverlapSphere(targetPosition + clipOffset, 0.1f).Length > 0;
             if (!isClip)
                 return;
         }*/
    }
}