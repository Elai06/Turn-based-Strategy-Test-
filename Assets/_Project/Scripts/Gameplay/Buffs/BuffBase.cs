using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public abstract class BuffBase
    {
        protected IUnit _usingBuffUnit;
        protected IUnit _enemy;

        protected BuffConfigData _buffData;

        public int Round;
        public EBuffName EName { get; private set; }

        protected BuffParameterData _buffParameter;
        protected BuffParameterData _deBuffParameter;

        public BuffBase(IUnit usingBaff, IUnit enemy, BuffConfigData data)
        {
            _usingBuffUnit = usingBaff;
            _enemy = enemy;
            _buffData = data;
            EName = data.EBuffName;
            _buffParameter = _buffData.IncreaseParameter;
            _deBuffParameter = _buffData.DecreaseParameter;
        }

        public virtual void Activate()
        {
            Round = _buffData.Rounds;
        }

        public virtual void Deactivate()
        {

        }
    }
}