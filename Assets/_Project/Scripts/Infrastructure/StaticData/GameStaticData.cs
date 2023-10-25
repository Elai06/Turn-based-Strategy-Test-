using System;
using _Project.Scripts.Gameplay.Buffs;
using _Project.Scripts.Gameplay.Configs;
using _Project.Scripts.Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Configs/GameStaticData")]
    public class GameStaticData : ScriptableObjectInstaller
    {
        [SerializeField] private WindowsStaticData _windowsStaticData;
        [SerializeField] private UnitsConfig _unitsConfig;
        [SerializeField] private BuffsConfig _buffsConfig;

        public WindowData GetWindowData(WindowType windowType)
        {
            return _windowsStaticData.GetWindows().Find(x => x.WindowType == windowType);
        }
        
        public UnitsConfig GetUnitsConfig()
        {
            return _unitsConfig;
        }

        public BuffsConfig GetBuffsConfig()
        {
            return _buffsConfig;
        }
    }
}