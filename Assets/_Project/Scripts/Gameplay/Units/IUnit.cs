using _Project.Scripts.Gameplay.Configs;

namespace _Project.Scripts.Gameplay.Units
{
    public interface IUnit
    {
        void Attack(UnitBase target);
        void TakeDamage(int damage);
        void EnchanceParameter(EParametersType type, int value);
        void DecreaseParameter(EParametersType type, int value);
        int GetParameter(EParametersType type);
    }
}