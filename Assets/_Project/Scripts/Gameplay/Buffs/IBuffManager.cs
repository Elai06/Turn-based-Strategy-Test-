using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Buffs
{
    public interface IBuffManager
    {
        void ActivateBuff(EAttackingSide attackingSide, UnitBase usedBuffUnitBase, UnitBase enemy);
        void UpdateRound(int round);
        event Action<EAttackingSide, BuffBase> ActivatedBuff;
        event Action<EAttackingSide, BuffBase> DeactivatedBuff;
        Dictionary<EBuffName, BuffBase> LeftSideBuffs { get; }
        Dictionary<EBuffName, BuffBase> RightSideBuffs { get; }
        event Action<EAttackingSide, BuffBase> UpdateRounds;
    }
}