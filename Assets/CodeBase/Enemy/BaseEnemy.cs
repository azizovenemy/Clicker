using UnityEngine;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(CharacterController))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        
        private CharacterController _controller;

        private void Awake() => 
            _controller = GetComponent<CharacterController>();

        private void Update()
        {
            var movementVector = Vector3.zero;
            movementVector += Physics.gravity;
            
            _controller.Move(movementVector * (movementSpeed * Time.deltaTime));
        }

        public float CalculateMaxHealth(int index) => 
            index == 0 ? Constants.Increase : Constants.Increase * (index % 5 == 0 ? index * 2f : index);
    }
}