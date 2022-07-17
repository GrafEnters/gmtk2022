using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance;

    public Image oldHpCube, newHpCube;
    public Sprite[] hpCubeSprites;

    private bool canReload = false;

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
}