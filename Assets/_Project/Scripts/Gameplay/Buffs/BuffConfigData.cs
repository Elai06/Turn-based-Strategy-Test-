using System;
using _Project.Scripts.Gameplay.Enums;

namespace _Project.Scripts.Gameplay.Buffs
{
    [Serializable]
    public class BuffConfigData
    {
        public EBuffName EBuffName;
        public int Rounds;
        public BuffParameterData IncreaseParameter;
        public BuffParameterData DecreaseParameter;
    }
}