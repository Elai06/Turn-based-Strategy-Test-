using System;
using _Project.Scripts.Gameplay.Units;

namespace _Project.Scripts.Gameplay.Configs
{
    [Serializable]
    public class UnitParameterData
    {
        public EParametersType Type;
        public int MaxValue = 100;
        public int Value;
    }
}