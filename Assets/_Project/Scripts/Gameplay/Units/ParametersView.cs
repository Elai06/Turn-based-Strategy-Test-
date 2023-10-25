using _Project.Scripts.Gameplay.Configs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class ParametersView : MonoBehaviour
    {
        [SerializeField] private UnitBase _unitBase;
        [SerializeField] private TextMeshPro _healthText;
        [SerializeField] private TextMeshPro _damageText;
        [SerializeField] private TextMeshPro _armorText;
        [SerializeField] private TextMeshPro _vampirismText;

        private void Start()
        {
            SetArmor();
            SetHealth();
            SetDamage();
            SetVampirism();
        }

        private void OnEnable()
        {
            _unitBase.ParamaterChanged += OnParametersChanged;
        }

        private void OnDisable()
        {
            _unitBase.ParamaterChanged -= OnParametersChanged;
        }

        private void OnParametersChanged(EParametersType type, int value)
        {
            var configData = _unitBase.GetParameterConfigData(type);
            switch (type)
            {
                case EParametersType.Armor:
                    _armorText.text = $"Armor {value}/{configData.MaxValue}";
                    break;
                case EParametersType.Damage:
                    _damageText.text = $"Damage {value}/{configData.MaxValue}";
                    break;
                case EParametersType.Health:
                    _healthText.text = $"Health {value}/{configData.MaxValue}";
                    break;
                case EParametersType.Vampirism:
                    _vampirismText.text = $"Vampirism {value}/{configData.MaxValue}";
                    break;
            }
        }

        private void SetHealth()
        {
            HealthChanged(_unitBase.GetParameter(EParametersType.Health));
        }

        private void SetArmor()
        {
            var configData = _unitBase.GetParameterConfigData(EParametersType.Armor);
            _armorText.text = $"Armor {configData.Value}/{configData.MaxValue}";
        }

        private void SetDamage()
        {
            var configData = _unitBase.GetParameterConfigData(EParametersType.Damage);
            _damageText.text = $"Damage {configData.Value}/{configData.MaxValue}";
        }
        
        private void SetVampirism()
        {
            var configData = _unitBase.GetParameterConfigData(EParametersType.Vampirism);
            _vampirismText.text = $"Vampirism {configData.Value}/{configData.MaxValue}";
        }

        private void HealthChanged(int health)
        {
            var configData = _unitBase.GetParameterConfigData(EParametersType.Health);
            _healthText.text = $"Health {health}/{configData.MaxValue}";
        }
    }
}