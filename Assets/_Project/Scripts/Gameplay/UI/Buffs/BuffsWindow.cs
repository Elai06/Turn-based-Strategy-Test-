using _Project.Scripts.Gameplay.Buffs;
using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.UI.Buffs
{
    public class BuffsWindow : Window
    {
        [SerializeField] private BuffViewInitializer _buffViewInitializer;

        private IBuffManager _buffManager;
        
        [Inject]
        private void Construct(IBuffManager buffManager)
        {
            _buffManager = buffManager;
        }

        private void OnEnable()
        {
            _buffViewInitializer.Initialize(_buffManager);
        }
    }
}