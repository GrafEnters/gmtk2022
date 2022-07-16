using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour {
    public float POVNormal = 40, POVAIM = 20;
    public float lerpSpeed = 0.3f;
    
    [Header("Other")]   
    public Transform NormalMode;
    public Transform AimMode;
    private Transform cameraTransform;
    private Camera camera;
    private float targetPov;
   
 
    private void Awake() {
        cameraTransform = Camera.main.transform;
        camera = Camera.main;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(1)) {
            targetPov = POVAIM;
            cameraTransform.SetParent(AimMode);
        } else {
            targetPov = POVNormal;
            cameraTransform.SetParent(NormalMode);
        }

        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetPov,lerpSpeed);
        cameraTransform.localRotation = Quaternion.Lerp( cameraTransform.localRotation, Quaternion.identity, lerpSpeed);
        cameraTransform.localPosition = Vector3.Lerp( cameraTransform.localPosition, Vector3.zero, lerpSpeed);
    }
}
