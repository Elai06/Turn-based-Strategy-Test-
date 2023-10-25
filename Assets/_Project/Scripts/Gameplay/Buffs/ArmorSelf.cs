using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class ArmorSelf : BuffBase
    {
        public ArmorSelf(IUnit usingBaff, IUnit enemy, BuffConfigData data) : base(usingBaff, enemy, data)
        {
        }

        public override void Activate()
        {
            base.Activate();

            _usingBuffUnit.EnchanceParameter(EParametersType.Armor, _buffData.IncreaseParameter.Value);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            _usingBuffUnit.DecreaseParameter(EParametersType.Armor, _buffData.IncreaseParameter.Value);
        }
    }
}