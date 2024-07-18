using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FTUEManager : MonoBehaviour {
    public PlayerController PlayerController;
    public ShooterController ShooterController;
    public CameraAim CameraAim;
    public MouseLook VerticalMouseLook;
    public MouseLook HorizontalMouseLook;
    public GameObject Interface;
    public float cutsceneTime = 3f;

    private Transform cameraTransform;

    public Transform DreamGame;
    public Transform finalGamePosition;
    public Transform middleGamePosition;

    private bool isGameStarted;

    public Animation ftueExplainAnimation;
    public Animation anyButton;

    private void Awake() {
        cameraTransform = Camera.main.transform;
        StopGame();
    }

    private void Update() {
        if (Input.anyKeyDown && !isGameStarted ) {
            isGameStarted = true;
            StartCoroutine(MoveDreamGame());
        }
    }

    private void StopGame() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        VerticalMouseLook.enabled = false;
        HorizontalMouseLook.enabled = false;
        CameraAim.enabled = false;
        ShooterController.lockShooting = true;
        PlayerController.enabled = false;
    }

    private void StartGame() {
        PlayerController.enabled = true;
        VerticalMouseLook.enabled = true;
        HorizontalMouseLook.enabled = true;
        CameraAim.enabled = true;
        ShooterController.lockShooting = false;
        Interface.SetActive(true);
        ftueExplainAnimation.Play("show_ExplainDialog");
    }

    private IEnumerator MoveDreamGame() {
        anyButton.Play("hide");
        float curTime = 0;
        Vector3 startPos = DreamGame.position;
        Quaternion startrot = DreamGame.rotation;
        while (curTime <= 2.5f) {
            curTime += Time.deltaTime;

            DreamGame.position =
                Vector3.Lerp(startPos, middleGamePosition.position, curTime / 2.5f);
            DreamGame.rotation =
                Quaternion.Lerp(startrot, middleGamePosition.rotation, curTime / 2.5f);

            CameraAim.transform.forward = DreamGame.position - CameraAim.transform.position;
            yield return new WaitForEndOfFrame();
        }
        startPos = DreamGame.position;
        startrot = DreamGame.rotation;
        curTime = 0;
        while (curTime <= 2.5f) {
            curTime += Time.deltaTime;

            DreamGame.position =
                Vector3.Lerp(startPos, finalGamePosition.position, curTime / 2.5f);
            DreamGame.rotation =
                Quaternion.Lerp(startrot, finalGamePosition.rotation, curTime / 2.5f);

            CameraAim.transform.forward = DreamGame.position - CameraAim.transform.position;
            yield return new WaitForEndOfFrame();
        }

        CameraAim.enabled = true;
        float lerpSpeed = CameraAim.lerpSpeed;
        CameraAim.lerpSpeed = 0.05f;
        yield return new WaitForSeconds(2);
        CameraAim.lerpSpeed = lerpSpeed;
        StartGame();
    }
}