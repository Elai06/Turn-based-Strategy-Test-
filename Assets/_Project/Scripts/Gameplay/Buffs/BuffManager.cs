using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Infrastructure.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;
using UnitBase = _Project.Scripts.Gameplay.Units.UnitBase;

namespace _Project.Scripts.Gameplay.Buffs
{
    public class BuffManager : IBuffManager
    {
        private const int MAX_BUFFS = 2;

        public event Action<EAttackingSide, BuffBase> ActivatedBuff;
        public event Action<EAttackingSide, BuffBase> DeactivatedBuff;
        public event Action<EAttackingSide, BuffBase> UpdateRounds;

        private readonly BuffsConfig _buffsConfig;
        private readonly UnitSpawner _unitSpawner;

        public Dictionary<EBuffName, BuffBase> LeftSideBuffs { get; private set; } = new();
        public Dictionary<EBuffName, BuffBase> RightSideBuffs { get; private set; } = new();

        public BuffManager(GameStaticData gameStaticData, UnitSpawner unitSpawner)
        {
            _buffsConfig = gameStaticData.GetBuffsConfig();
            _unitSpawner = unitSpawner;
            _unitSpawner.OnRespawnUnits += RespawnUnits;
        }

        public void ActivateBuff(EAttackingSide attackingSide, UnitBase usedBuffUnitBase, UnitBase enemy)
        {
            var buffs = attackingSide == EAttackingSide.Left
                ? LeftSideBuffs
                : RightSideBuffs;
            var buffData = GetRandomBuffConfigData(buffs);

            CreateBuff(attackingSide, buffs,
                usedBuffUnitBase, enemy, buffData);

            Debug.Log($"{usedBuffUnitBase.name} used buff {buffData.EBuffName}");
        }

        private void CreateBuff(EAttackingSide attackingSide, Dictionary<EBuffName, BuffBase> buffs,
            UnitBase usedBuffUnitBase, UnitBase enemy,
            BuffConfigData buffData)
        {
            if (buffs.Count >= MAX_BUFFS || buffs.ContainsKey(buffData.EBuffName)) return;

            switch (buffData.EBuffName)
            {
                case EBuffName.ArmorDestruction:
                    buffs.Add(buffData.EBuffName, new ArmorDesctruction(usedBuffUnitBase, enemy, buffData));
                    break;
                case EBuffName.ArmorSelf:
                    buffs.Add(buffData.EBuffName, new ArmorSelf(usedBuffUnitBase, enemy, buffData));
                    break;
                case EBuffName.DoubleDamage:
                    buffs.Add(buffData.EBuffName, new DoubleDamage(usedBuffUnitBase, enemy, buffData));
                    break;
                case EBuffName.VampirismDecrease:
                    buffs.Add(buffData.EBuffName, new VampirismDecrease(usedBuffUnitBase, enemy, buffData));
                    break;
                case EBuffName.VampirismSelf:
                    buffs.Add(buffData.EBuffName, new VampirismSelf(usedBuffUnitBase, enemy, buffData));
                    break;
            }


            buffs[buffData.EBuffName].Activate();
            ActivatedBuff?.Invoke(attackingSide, buffs[buffData.EBuffName]);
        }

        private void DeactivateBuff(EBuffName eName, Dictionary<EBuffName, BuffBase> buffs,
            EAttackingSide eAttackingSide)
        {
            var buff = buffs[eName];
            buff.Deactivate();
            buffs.Remove(eName);
            DeactivatedBuff?.Invoke(eAttackingSide, buff);
        }

        public void UpdateRound(int round)
        {
            UpdateBuff(LeftSideBuffs, EAttackingSide.Left);
            UpdateBuff(RightSideBuffs, EAttackingSide.Right);
        }

        private void UpdateBuff(Dictionary<EBuffName, BuffBase> buffs, EAttackingSide attackingSide)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                var buff = buffs.ToList()[i].Value;
                buff.Round--;
                if (buff.Round <= 0)
                {
                    DeactivateBuff(buff.EName, buffs, attackingSide);
                    i--;
                }
                else
                {
                    UpdateRounds?.Invoke(attackingSide, buff);
                }
            }
        }

        private BuffConfigData GetRandomBuffConfigData(Dictionary<EBuffName, BuffBase> buffBases)
        {
            var randomIndex = Random.Range(0, _buffsConfig.Buffs.Count);
            var buffData = _buffsConfig.Buffs[randomIndex];

            while (buffBases.ContainsKey(buffData.EBuffName))
            {
                buffData = _buffsConfig.Buffs[Random.Range(0, _buffsConfig.Buffs.Count)];
            }

            return buffData;
        }

        private void RespawnUnits()
        {
            LeftSideBuffs?.Clear();
            RightSideBuffs?.Clear();
        }
    }
}