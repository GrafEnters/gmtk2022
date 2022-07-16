using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour {
    [SerializeField]
    protected Bullet Bullet;
    
  
    public int bulletsAmount = 1;
    public int maxBulletsAmount = 1;
    public float recallTime = 0.3f;
    
    private bool canShoot;

    public void Equip() {
        canShoot = true;
        Reload();
    }
    public void Shoot(Ray ray) {
        if (canShoot && bulletsAmount>0) {
            ShootAbility(ray);
            bulletsAmount--;
            bulletsAmount = Mathf.Max(0, bulletsAmount);
            StartCoroutine(Recall());
        }
    }

    protected virtual void ShootAbility(Ray ray) {
        
    }

    public IEnumerator Recall() {
        canShoot = false;
        yield return new WaitForSeconds(recallTime);
        canShoot = true;
    }
    
    public void Reload() {
        bulletsAmount = maxBulletsAmount;
    }
}
