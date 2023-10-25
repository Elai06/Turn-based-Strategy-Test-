using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class VampirismSelf : BuffBase
    {
        private int _beginningArmorValue;

        public VampirismSelf(IUnit usingBaff, IUnit enemy, BuffConfigData data) : base(usingBaff, enemy, data)
        {
        }

        public override void Activate()
        {
            base.Activate();
            _beginningArmorValue = _usingBuffUnit.GetParameter(EParametersType.Armor);
            _usingBuffUnit.EnchanceParameter(EParametersType.Vampirism, _buffData.IncreaseParameter.Value);
            _usingBuffUnit.DecreaseParameter(EParametersType.Armor, _buffData.DecreaseParameter.Value);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            
            _usingBuffUnit.DecreaseParameter(EParametersType.Vampirism, _buffData.IncreaseParameter.Value);
            
            if(_beginningArmorValue == 0) return;
            _usingBuffUnit.EnchanceParameter(EParametersType.Armor, _buffData.DecreaseParameter.Value);
        }
    }
}