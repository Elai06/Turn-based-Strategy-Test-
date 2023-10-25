using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Buffs
{
    [CreateAssetMenu(menuName = "Configs/BuffsConfig", fileName = "BuffConfig", order = 0)]
    public class BuffsConfig : ScriptableObject
    {
        [SerializeField] private List<BuffConfigData> _buffs;
        
        public List<BuffConfigData> Buffs => _buffs;
    }
}