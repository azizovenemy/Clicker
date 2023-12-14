using CodeBase.Logic.Upgrades;
using CodeBase.StaticData;
using System;
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
            index == 0 ? Constants.Increase : (Constants.Increase + (index % 5 == 0 ? index * Constants.Increase : index)) * 1.24f;

        //private float CalculateReward(int index) => 
        //    (Constants.Increase + (index % 5 == 0 ? index * Constants.Increase : index)) * CalculateRewardByUpgrade();

        //private float CalculateRewardByUpgrade() => 
        //    Upgrades.Instance.FindExists(EUpgradeTypeId.MoneyRewardIncrease)
        //        ? Upgrades.Instance.GetUpgradeCount(EUpgradeTypeId.MoneyRewardIncrease) * 1.54f
        //        : 0.0f;
    }
}