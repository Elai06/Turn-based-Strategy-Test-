using System;

namespace _Project.Scripts.Gameplay.Battle
{
    public interface IBattleManager
    {
        void Initialize();
        int CurrentRound { get; }
        event Action<int> UpdateRound;
        event Action OnResetRounds;
        event Action<EAttackingSide> SwitchedAtackingSide;
    }
}