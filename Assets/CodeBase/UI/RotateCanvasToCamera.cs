using UnityEngine;

namespace CodeBase.UI
{
    class RotateCanvasToCamera : MonoBehaviour
    {
        private void Start() => 
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
}