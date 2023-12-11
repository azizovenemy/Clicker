using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.UI
{
    public class RotateToCamera : MonoBehaviour
    {
        private void Start() => 
            transform.LookAt(CameraVector());

        private Vector3 CameraVector() =>
            new(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
    }
}
