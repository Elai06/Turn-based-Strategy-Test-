using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.StateMachine.Sates;
using Infrastructure.Windows;
using SirGames.Scripts.Infrastructure.StateMachine;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.StateMachine.States
{
    public class GameState : IState
    {
        private IStateMachine _stateMachine;
        private readonly IBattleManager _battleManager;
        private readonly UnitSpawner _unitSpawner;

        public GameState(IBattleManager battleManager, UnitSpawner unitSpawner)
        {
            _battleManager = battleManager;
            _unitSpawner = unitSpawner;

        }

        public void Initialize(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;

            _unitSpawner.Initialize();
            _battleManager.Initialize();
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}