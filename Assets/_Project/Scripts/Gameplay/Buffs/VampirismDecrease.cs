using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class VampirismDecrease : BuffBase
    {
        private int _beginningVampirismValue;
        
        public VampirismDecrease(IUnit usingBaff, IUnit enemy, BuffConfigData data) : base(usingBaff, enemy, data)
        {
        }

        public override void Activate()
        {
            base.Activate();
            _beginningVampirismValue = _usingBuffUnit.GetParameter(EParametersType.Vampirism);
            _enemy.DecreaseParameter(EParametersType.Vampirism, _buffData.DecreaseParameter.Value);
        }

        public override void Deactivate()
        {
            base.Deactivate();

            if (_beginningVampirismValue == 0)
                return;

            _enemy.EnchanceParameter(EParametersType.Vampirism, _buffData.DecreaseParameter.Value);
        }
    }
}