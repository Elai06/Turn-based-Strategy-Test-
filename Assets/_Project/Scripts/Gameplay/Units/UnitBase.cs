using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Configs;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class UnitBase : MonoBehaviour, IUnit
    {
        public event Action OnDied;
        public event Action Attacked;
        public event Action<EParametersType, int> ParamaterChanged;

        [SerializeField] private ApplyDamageEffect _damageEffect;

        private readonly Dictionary<EParametersType, int> _parameters = new();
        private readonly Dictionary<EParametersType, UnitParameterData> _parametersConfigData = new();

        public bool IsDied => CheckIsDead();

        public void SetAttributes(List<UnitParameterData> parametersData, EUnitType type)
        {
            foreach (var parameter in parametersData)
            {
                _parameters.Add(parameter.Type, parameter.Value);
                _parametersConfigData.Add(parameter.Type, parameter);
            }
        }

        public void Attack(UnitBase target)
        {
            if (target.IsDied) return;

            target.TakeDamage(_parameters[EParametersType.Damage]);
            DamageBasedOnVampirism(target.TakeDamageBasedOnArmor(_parameters[EParametersType.Damage]));
            Attacked?.Invoke();
        }

        public void TakeDamage(int damage)
        {
            if (!IsDied)
            {
                _parameters[EParametersType.Health] -= TakeDamageBasedOnArmor(damage);
                _damageEffect.Effect();

                if (IsDied)
                {
                    Died();
                }

                ParamaterChanged?.Invoke(EParametersType.Health, _parameters[EParametersType.Health]);
            }
        }

        private void Died()
        {
            gameObject.SetActive(false);
            OnDied?.Invoke();
        }

        private int TakeDamageBasedOnArmor(int damage)
        {
            return damage - (int)((float)damage / 100 * _parameters[EParametersType.Armor]);
        }

        private void DamageBasedOnVampirism(int damage)
        {
            EnchanceParameter(EParametersType.Health,
                (int)((float)damage / 100 * _parameters[EParametersType.Vampirism]));
        }

        private bool CheckIsDead()
        {
            return _parameters[EParametersType.Health] <= 0;
        }

        public void EnchanceParameter(EParametersType type, int value)
        {
            if (type == EParametersType.None) return;

            _parameters[type] += value;
            
            if (_parameters[type] > GetParameterConfigData(type).MaxValue)
            {
                _parameters[type] = GetParameterConfigData(type).MaxValue;
            }

            ParamaterChanged?.Invoke(type, _parameters[type]);
        }

        public void DecreaseParameter(EParametersType type, int value)
        {
            if (type == EParametersType.None) return;
            
                _parameters[type] -= value;

                if (_parameters[type] < 0)
                {
                    _parameters[type] = 0;
                }
                
                ParamaterChanged?.Invoke(type, _parameters[type]);
            
        }

        public int GetParameter(EParametersType type)
        {
            if (_parameters.TryGetValue(type, out var parameter))
            {
                return parameter;
            }

            Debug.Log($"Данного параметра у Unit нету");
            return 0;
        }

        public UnitParameterData GetParameterConfigData(EParametersType type)
        {
            return _parametersConfigData[type];
        }
    }
}