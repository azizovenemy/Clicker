using System;
using UnityEngine;

namespace CodeBase.UI
{
    public class RotateCanvasToCamera : MonoBehaviour
    {
        private void Start()
        {
            // ReSharper disable once PossibleNullReferenceException
            transform.LookAt(Camera.main.transform);
        }
    }
}
