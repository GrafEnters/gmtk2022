using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    [Header("HP")]
    public Image oldHpCube, newHpCube;

    public Sprite[] hpCubeSprites;

    [Header("Weapons")]
    public WeaponUIData[] WeaponUIDatas;

    public GridLayoutGroup bulletsGrid;
    public RectTransform WeaponHolder;

    [Header("Compas")]
    public Transform compas;

    [Header("Timer")]
    public TextMeshProUGUI TimerText;

    private Animation _weaponAnimation;

    private void Awake() {
        Instance = this;
    }

    public AnimationClip[] Animations;
    public Animation hpCounterAnimation;

    public void ChangeHp(int newHp) {
        oldHpCube.sprite = hpCubeSprites[newHp + 1];
        newHpCube.sprite = hpCubeSprites[newHp];

        hpCounterAnimation.clip = Animations[newHp == 0 ? 0 : 1];
        hpCounterAnimation.Play();
    }

    public void ChangeWeapon(int newWeapon) {
        return;
        WeaponUIData curData = WeaponUIDatas[newWeapon];
        bulletsGrid.cellSize = curData.bulletSize;
        if (WeaponHolder.childCount > 0) {
            Destroy(WeaponHolder.GetChild(0));
        }

        _weaponAnimation = Instantiate(curData.WeaponPrefab, WeaponHolder);
        _weaponAnimation.Play("equip_animation");
    }

    public void Shoot() {
        return;
        _weaponAnimation.Play("shoot_animation");
    }
}

[Serializable]
public class WeaponUIData {
    public Animation WeaponPrefab;
    public Vector2 bulletSize;
}