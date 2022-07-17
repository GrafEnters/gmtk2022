using UnityEngine;

public class WeaponsHolder : MonoBehaviour {
    [SerializeField]
    private Transform target;
    private void LateUpdate() {
        Transform transform1 = transform;
       transform1.forward = target.position - transform1.position;
    }
}
