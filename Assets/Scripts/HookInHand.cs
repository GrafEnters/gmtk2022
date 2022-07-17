using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookInHand : MonoBehaviour
{
    public void Reload() {
        ShooterController.instance.ReloadHook();
    }
}
