using System;

namespace _Project.Scripts.Gameplay.Battle
{
    public interface IBattleSystem
    {
        void Attack(EAttackingSide eAttackingSide);
        void UseBuff(EAttackingSide eAttackingSide);
        event Action<EAttackingSide> StepFinished;
    }
}