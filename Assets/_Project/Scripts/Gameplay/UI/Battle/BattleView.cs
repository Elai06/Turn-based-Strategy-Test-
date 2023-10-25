using System;
using _Project.Scripts.Gameplay.Battle;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.UI.Battle
{
    public class BattleView : MonoBehaviour
    {
        public event Action<EAttackingSide> OnAttacking;
        public event Action<EAttackingSide> OnUseBaff;

        [SerializeField] private GameplayButtons _leftGamePlayButtons;
        [SerializeField] private GameplayButtons _rightGamePlayButtons;
        [SerializeField] private TextMeshProUGUI _roundText;

        private void OnEnable()
        {
            _leftGamePlayButtons.OnUseBaff += UseBaff;
            _leftGamePlayButtons.OnAttacking += Attacking;
            _rightGamePlayButtons.OnUseBaff += UseBaff;
            _rightGamePlayButtons.OnAttacking += Attacking;
        }

        private void OnDisable()
        {
            _leftGamePlayButtons.OnUseBaff -= UseBaff;
            _leftGamePlayButtons.OnAttacking -= Attacking;
            _rightGamePlayButtons.OnUseBaff -= UseBaff;
            _rightGamePlayButtons.OnAttacking -= Attacking;
        }

        private void Attacking(EAttackingSide attackingSide)
        {
            OnAttacking?.Invoke(attackingSide);
        }

        private void UseBaff(EAttackingSide attackingSide)
        {
            OnUseBaff?.Invoke(attackingSide);
        }

        public void SetRoundText(int round)
        {
            _roundText.text = $"Round {round + 1}";
        }

        public void ShowBuffButton(EAttackingSide attackingSide, bool isActive)
        {
            if (attackingSide == EAttackingSide.Left)
            {
                _leftGamePlayButtons.SwitchBuffButton(isActive);
            }
            else
            {
                _rightGamePlayButtons.SwitchBuffButton(isActive);
            }
        }

        public void ShowGameplayButtons(EAttackingSide eAttackingSide)
        {
            if (eAttackingSide == EAttackingSide.Left)
            {
                _leftGamePlayButtons.gameObject.SetActive(true);
                _rightGamePlayButtons.gameObject.SetActive(false);
            }
            else
            {
                _leftGamePlayButtons.gameObject.SetActive(false);
                _rightGamePlayButtons.gameObject.SetActive(true);
            }
        }
    }
}