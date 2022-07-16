using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEManager : MonoBehaviour {
    public PlayerController PlayerController;
    public ShooterController ShooterController;
    public CameraAim CameraAim;
    public MouseLook VerticalMouseLook;
    public MouseLook HorizontalMouseLook;

    public float cutsceneTime = 3f;

    private void Awake() {
        StartCoroutine(FirstCutScene());
    }

    private IEnumerator FirstCutScene() {
       //PlayerController.LockControls = true;
        VerticalMouseLook.enabled = false;
        CameraAim.enabled = false;
        ShooterController.lockShooting = true;
        yield return new WaitForSeconds(cutsceneTime);
        //PlayerController.LockControls = false;
        VerticalMouseLook.enabled = true;
        CameraAim.enabled = true;
        ShooterController.lockShooting = false;
    }
}