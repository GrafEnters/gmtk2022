using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    public float speed;

    public float sideSpeed;

    private float _gravityAcceleration;
    public float gravityValue = 3f;
    public float dashTime = 1f;
    public float dashSpeed = 1f;
    public float dashheight = 1f;
    public float dashHorizontalSpeed = 1f;

    [Header("Other")]
    public LayerMask groundLayerMask;

    public ShooterController ShooterController;
    public Transform groundSphere;

    public CharacterController Controller;

    public Transform body;

    private bool isAutoJump;
    private Vector3 move;
    private bool LockControls;
    public bool isGrounded;
    public bool isDashing;
    public float dashCurrentTime = 0;
    public Vector3 dashVector;
    public AnimationCurve dashCurve;
    public AnimationCurve rotateCurve;
    public Quaternion targetRotation;
    public Quaternion beforeRotation;
    public int currentSideFacingTop;
    public Quaternion accumulatedRotations;
    public static Vector3 BodyPosition;
    public List<Transform> sides;

    private void Awake() {
        BodyPosition = body.transform.position;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        ShooterController.ChangeWeapon(0);
    }

    void Update() {
        BodyPosition = body.transform.position;
        move = Vector3.zero;
        if (!LockControls) {
            if (!isDashing) {
                Move();
            }

            if (Input.GetButtonDown("Jump") && isGrounded && !isDashing) {
                StartDash();
            }

            if (isGrounded && !isDashing && isAutoJump) {
                StartDash();
            }
        }

        if (isDashing) {
            DashCoroutine();
        }

        Gravity();
        Controller.Move(move * Time.deltaTime);
    }

    private void Move() {
        move += transform.right * (Input.GetAxis("Horizontal") * sideSpeed);
        move += transform.forward * (Input.GetAxis("Vertical") * speed);
    }

    private void Gravity() {
        isGrounded = Physics.CheckSphere(groundSphere.position, groundSphere.localScale.x, groundLayerMask);

        if (isGrounded && _gravityAcceleration > 0) {
            _gravityAcceleration = 0f;
        }

        _gravityAcceleration += gravityValue * Time.deltaTime;
        move += Vector3.down * _gravityAcceleration;
    }

    private void StartDash() {
        isDashing = true;
        dashVector = Vector3.zero;
        dashCurrentTime = 0;
        beforeRotation = body.localRotation;
        Vector3 sideRotation = Vector3.zero;

        Matrix4x4 matrix4X4 = body.worldToLocalMatrix;

        if (Input.GetAxis("Horizontal") != 0) {
            dashVector = transform.right * (Input.GetAxis("Horizontal") * dashHorizontalSpeed);
            sideRotation = Input.GetAxis("Horizontal") > 0
                ? matrix4X4.MultiplyVector(body.forward) * -1
                : matrix4X4.MultiplyVector(body.forward);
            ;
        } else if (Input.GetAxis("Vertical") != 0) {
            dashVector = transform.forward * (Input.GetAxis("Vertical") * dashHorizontalSpeed);
            sideRotation = Input.GetAxis("Vertical") > 0
                ? matrix4X4.MultiplyVector(body.right)
                : matrix4X4.MultiplyVector(body.forward) * -1;
        }

        dashVector += Vector3.up * dashheight;
        accumulatedRotations *= Quaternion.Euler(sideRotation * 90);
        targetRotation = beforeRotation * Quaternion.Euler(sideRotation * 90);
        //targetRotation =  Quaternion.FromToRotation(Vector3.up, sideRotation);
    }

    private void GetDamage() {
    }

    private void DashCoroutine() {
        if (dashCurrentTime < dashTime) {
            //dashVector.y = dashheight * Mathf.Cos(dashCurrentTime / dashTime);
            float dashPercent = dashCurrentTime / dashTime;
            move += dashVector * dashCurve.Evaluate(dashPercent);
            body.transform.localRotation =
                Quaternion.Slerp(beforeRotation, targetRotation, dashCurve.Evaluate(dashPercent));
            dashCurrentTime += Time.deltaTime;
        } else if (!isGrounded) {
            move += dashVector;
        } else {
            body.transform.localRotation = targetRotation;
            GetRotationFromSide();
            ShooterController.ChangeWeapon(currentSideFacingTop);
            isDashing = false;
        }
    }

    private void GetRotationFromSide() {
        float yPos = 0;
        int index = 0;
        foreach (Transform sideTransform in sides.Where(sideTransform => sideTransform.position.y > yPos)) {
            index = sides.IndexOf(sideTransform);
            yPos = sideTransform.position.y;
        }

        currentSideFacingTop = index;
    }
}

[Serializable]
public class Side {
    public int[] neighbours = new[] {0, 0, 0, 0};
}