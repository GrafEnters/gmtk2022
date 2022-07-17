using UnityEngine;

public class ShooterController : MonoBehaviour {
    public static ShooterController instance;

    public float maxTargetDistance = 100;

    [SerializeField]
    private Transform _weaponHolder;

    [SerializeField]
    private Transform _bulletsHolder;

    [HideInInspector]
    public Animation currentWeaponInHand;

    [SerializeField]
    private Transform _shootPoint;

    public static Transform BulletsHolder;

    public AudioClip shoot;
    public AudioSource AudioSource;

    [SerializeField]
    protected LayerMask shootMask;

    [SerializeField]
    private WeaponBase[] weapons;

    [HideInInspector]
    public WeaponBase curWeapon;

    public Transform TargetTransform;

    [SerializeField]
    private LineRenderer _lineRenderer;

    public bool canChangeByNumbers = false;
    public bool lockShooting = false;

    private const string SHOOT_ANIMATION = "weaponInHand_shoot";

    private void Awake() {
        BulletsHolder = _bulletsHolder;
        instance = this;
    }

    private void MoveTarget() {
        Vector3 target;
        Ray raycastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raycastRay, out RaycastHit info, maxTargetDistance, shootMask)) {
            target = info.point;
        } else {
            target = raycastRay.GetPoint(maxTargetDistance);
        }

        TargetTransform.position = target;
        TargetTransform.forward = raycastRay.direction;
    }

    void Update() {
        if (lockShooting)
            return;

        MoveTarget();
        if (Input.GetMouseButton(0)) {
            if (curWeapon.bulletsAmount > 0) {
                Ray shootRay = new(_shootPoint.position, TargetTransform.position - _shootPoint.position);
                if (curWeapon.Shoot(shootRay)) ;
                {
                    AudioSource.clip = shoot;
                    AudioSource.Play();
                }
            }

            currentWeaponInHand.Play(SHOOT_ANIMATION);
        }

        if (canChangeByNumbers) {
            if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeWeapon(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeWeapon(3);
            if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeWeapon(4);
            if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeWeapon(5);
        }
    }

    public void ChangeWeapon(int weaponIndex) {
        curWeapon = weapons[weaponIndex];
        curWeapon.Equip();
        UIManager.Instance.ChangeWeapon(weaponIndex);
        if (currentWeaponInHand != null) {
            Destroy(currentWeaponInHand.gameObject);
        }

        currentWeaponInHand = Instantiate(curWeapon.WeaponPrefab, _weaponHolder);
        //AudioSource.clip = equip;
        AudioSource.Play();
    }

    private void DrawLine(Ray ray) {
        _lineRenderer.transform.position = ray.origin;
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, ray.origin);
        _lineRenderer.SetPosition(1, ray.GetPoint(100));
    }

    public void ReloadHook() {
        curWeapon.Equip();
        UIManager.Instance.AddBullet();
    }

    public int LeftAmmo => curWeapon.bulletsAmount;
}