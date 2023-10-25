using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Gameplay.Units;
using UnityEngine.Serialization;

namespace _Project.Scripts.Gameplay.Configs
{
    [Serializable]
    public class UnitConfigData
    {
        public List<UnitParameterData> ParametersData;
        public EUnitType EUnitType;
        [FormerlySerializedAs("Unit")] public UnitBase unitBase;
    }
}