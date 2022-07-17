using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour {
    [SerializeField]
    protected Bullet Bullet;

    [HideInInspector]
    public int bulletsAmount = 1;
    public int maxBulletsAmount = 1;
    public float recallTime = 0.3f;

    private bool _canShoot;

    public void Equip() {
        _canShoot = true;
        Reload();
    }

    public void Shoot(Ray ray) {
        if (_canShoot && bulletsAmount > 0) {
            ShootAbility(ray);
            bulletsAmount--;
            bulletsAmount = Mathf.Max(0, bulletsAmount);
            StartCoroutine(Recall());
        }
    }

    protected virtual void ShootAbility(Ray ray) {
        UIManager.Instance.Shoot();
    }

    private IEnumerator Recall() {
        _canShoot = false;
        yield return new WaitForSeconds(recallTime);
        _canShoot = true;
    }

    private void Reload() {
        bulletsAmount = maxBulletsAmount;
    }
}