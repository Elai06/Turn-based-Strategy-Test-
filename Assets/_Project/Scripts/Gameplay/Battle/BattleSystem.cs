using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Buffs;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Battle
{
    public class BattleSystem : IBattleSystem
    {
        public event Action<EAttackingSide> StepFinished;

        private readonly UnitSpawner _unitSpawner;
        private IBuffManager _buffManager;

        public BattleSystem(UnitSpawner unitSpawner, IBuffManager buffManager)
        {
            _unitSpawner = unitSpawner;
            _buffManager = buffManager;
        }

        public List<UnitBase> LeftUnits => _unitSpawner.LeftUnits;
        public List<UnitBase> RightUnits => _unitSpawner.RightUnits;

        public void Attack(EAttackingSide eAttackingSide)
        {
            if (eAttackingSide == EAttackingSide.Left)
            {
                LeftUnits[0].Attack(RightUnits[0]);
            }
            else
            {
                RightUnits[0].Attack(LeftUnits[0]);
            }
            
            StepFinish(eAttackingSide);
        }

        public void UseBuff(EAttackingSide eAttackingSide)
        {
            
            if (eAttackingSide == EAttackingSide.Left)
            {
                _buffManager.ActivateBuff(eAttackingSide,LeftUnits[0], RightUnits[0]);
            }
            else
            {
                _buffManager.ActivateBuff(eAttackingSide,RightUnits[0], LeftUnits[0]);

            }
            
            StepFinish(eAttackingSide);
        }

        private void StepFinish(EAttackingSide eAttackingSide)
        {
            StepFinished?.Invoke(eAttackingSide);
        }
    }
}