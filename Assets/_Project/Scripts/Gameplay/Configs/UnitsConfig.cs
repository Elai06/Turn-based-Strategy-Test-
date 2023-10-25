using UnityEngine;

namespace _Project.Scripts.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/UnitsConfig", fileName = "UnitsConfig", order = 0)]
    public class UnitsConfig : ScriptableObject
    {
        [SerializeField] private UnitConfigData _leftUnitConfig;
        [SerializeField] private UnitConfigData _rightUnitConfig;

        public UnitConfigData LeftUnit => _leftUnitConfig;
        public UnitConfigData RightUnit => _rightUnitConfig;
    }
}