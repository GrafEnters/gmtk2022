using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneManager : MonoBehaviour
{
    private bool canReload = false;
    
    public void CanReload() {
        canReload = true;
    }

    private void Update() {
        if (Input.anyKeyDown && canReload) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
