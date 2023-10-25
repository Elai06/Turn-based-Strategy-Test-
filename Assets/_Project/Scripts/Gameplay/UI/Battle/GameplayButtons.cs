using System;
using _Project.Scripts.Gameplay.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.UI.Battle
{
    public class GameplayButtons : MonoBehaviour
    {
        public event Action<EAttackingSide> OnAttacking;
        public event Action<EAttackingSide> OnUseBaff;
        
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _useBuffButton;
        [SerializeField] private EAttackingSide _eAttacking;

        private void OnEnable()
        {
            _attackButton.onClick.AddListener(Attacking);
            _useBuffButton.onClick.AddListener(UseBaff);
        }

        private void OnDisable()
        {
            _attackButton.onClick.RemoveListener(Attacking);
            _useBuffButton.onClick.RemoveListener(UseBaff);
        }

        public void SwitchBuffButton(bool isActive)
        {
            _useBuffButton.gameObject.SetActive(isActive);
        }

        private void Attacking()
        {
            OnAttacking?.Invoke(_eAttacking);
        }

        private void UseBaff()
        {
            OnUseBaff?.Invoke(_eAttacking);
        }
    }
}