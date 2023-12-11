using System;
using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        
        private CharacterController _controller;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var movementVector = Vector3.zero;
            movementVector += Physics.gravity;
            
            _controller.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        public float CalculateMaxHealth(int index)
        {
            if (index == 0) return Constants.Increase; 
            
            return (Constants.Increase + (index == 5 ? index * Constants.Increase : index)) * 1.24f;
        }
    }
}