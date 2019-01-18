using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private void FixedUpdate() {
        transform.LookAt(GameController.mainCamera);
    }
}
