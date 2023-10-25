using _Project.Scripts.Gameplay.Battle;
using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.UI.Battle
{
    public class BattleWindow : Window
    {
        [SerializeField] private BattleViewInititalizer _battleViewInititalizer;

        private IBattleSystem _battleSystem;

        [Inject]
        public void Construct(IBattleSystem battleSystem)
        {
            _battleSystem = battleSystem;
        }

        private void OnEnable()
        {
            _battleViewInititalizer.Initialize(_battleSystem);
        }
    }
}