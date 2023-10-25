using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class ArmorDesctruction : BuffBase
    {
        private int _beginningArmorValue;

        public ArmorDesctruction(IUnit usingBaff, IUnit enemy, BuffConfigData data) : base(usingBaff, enemy, data)
        {
        }

        public override void Activate()
        {
            base.Activate();
            _beginningArmorValue = _enemy.GetParameter(EParametersType.Armor);
            _enemy.DecreaseParameter(EParametersType.Armor, _buffData.DecreaseParameter.Value);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            if (_beginningArmorValue == 0) return;

            _enemy.EnchanceParameter(EParametersType.Armor, _buffData.DecreaseParameter.Value);
        }
    }
}