using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    public float speed;

    public float sideSpeed;
    public float maxVelocity = 10;
    public float dashHeight = 1f;
    public float dashHorizontalSpeed = 1f;

    [Header("Other")]
    public LayerMask groundLayerMask;

    public ShooterController ShooterController;
    public Transform groundSphere;

    public Transform body;

    private int _currentSideFacingTop;
    private Vector3 _move;
    public bool isGrounded;
    public static Vector3 BodyPosition;
    public Rigidbody rb;

    public Quaternion bodyTargetRotation;

    private static readonly Vector3[] SidesRotation = new[] {
        Vector3.zero,
        new Vector3(0, 0, -90),
        new Vector3(90, 0, 0),
        new Vector3(-90, 0, 0),
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 180),
    };

    private void Awake() {
        BodyPosition = body.transform.position;
        bodyTargetRotation = body.transform.localRotation;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        ShooterController.ChangeWeapon(0);
    }

    void Update() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            Dash();
        }
        BodyPosition = body.transform.position;
    }

    void FixedUpdate() {
        isGrounded = Physics.CheckSphere(groundSphere.position, groundSphere.lossyScale.x, groundLayerMask);
        Move();

        body.localRotation = Quaternion.Lerp(body.localRotation, bodyTargetRotation, 0.3f);
    }

    private void Move() {
        rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * sideSpeed), ForceMode.VelocityChange);
        rb.AddForce(transform.forward * (Input.GetAxis("Vertical") * speed), ForceMode.VelocityChange);
        Vector3 horSpeed = rb.velocity;
        horSpeed.y = 0;

        if (horSpeed.magnitude > maxVelocity) {
            horSpeed = horSpeed.normalized * maxVelocity;
            horSpeed.y = rb.velocity.y;
            rb.velocity = horSpeed;
        }
    }

    private void Dash() {
        Vector3 dashVector = Vector3.zero;
        if (Input.GetAxis("Vertical") != 0) {
            dashVector = transform.forward * (Input.GetAxis("Vertical") * dashHorizontalSpeed);
        } else if (Input.GetAxis("Horizontal") != 0) {
            dashVector = transform.right * (Input.GetAxis("Horizontal") * dashHorizontalSpeed);
        }

        dashVector += Vector3.up * dashHeight;
        rb.AddForce(dashVector, ForceMode.Impulse);

        ChangeSideToRandom();

        ShooterController.ChangeWeapon(_currentSideFacingTop);
    }

    private void ChangeSideToRandom() {
        body.transform.localRotation = bodyTargetRotation;
        int curSide = Random.Range(0, 6);
        while (curSide == _currentSideFacingTop) {
            curSide = Random.Range(0, 6);
        }

        _currentSideFacingTop = curSide;
        bodyTargetRotation = Quaternion.Euler(SidesRotation[_currentSideFacingTop]);
    }

    public void TakeDamage(Vector3 from) {
        
    }
}