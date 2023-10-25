using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class DoubleDamage : BuffBase
    {
        private int _doubleDamage;

        public DoubleDamage(IUnit usingBaff, IUnit enemy, BuffConfigData data) : base(usingBaff, enemy, data)
        {
        }

        public override void Activate()
        {
            base.Activate();

            _doubleDamage = _usingBuffUnit.GetParameter(EParametersType.Damage);
            _usingBuffUnit.EnchanceParameter(EParametersType.Damage, _doubleDamage);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _usingBuffUnit.DecreaseParameter(EParametersType.Damage, _doubleDamage);
        }
    }
}