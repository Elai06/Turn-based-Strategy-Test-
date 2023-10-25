using System;
using _Project.Scripts.Gameplay.Buffs;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;

namespace _Project.Scripts.Gameplay.Battle
{
    public class BattleManager : IBattleManager
    {
        public event Action<int> UpdateRound;
        public event Action OnResetRounds;
        public event Action<EAttackingSide> SwitchedAtackingSide;

        private readonly UnitSpawner _unitSpawner;
        private readonly IBattleSystem _battleSystem;
        private readonly IWindowService _windowService;
        private readonly IBuffManager _buffManager;

        private (bool isLeftStep, bool isRightStep) _steps;
        public int CurrentRound { get; private set; }

        public BattleManager(UnitSpawner unitSpawner, IBattleSystem battleSystem, IWindowService windowService,
            IBuffManager buffManager)
        {
            _unitSpawner = unitSpawner;
            _battleSystem = battleSystem;
            _windowService = windowService;
            _buffManager = buffManager;
        }

        public void Initialize()
        {
            _unitSpawner.OnRespawnUnits += OnRespawnUnits;
            _battleSystem.StepFinished += OnStepFinish;
            _windowService.Open(WindowType.Battle);
            _windowService.Open(WindowType.Buffs);
            StartRound();
        }

        private void StartRound()
        {
            SwitchAttackingSide(EAttackingSide.Left);
        }

        private void SwitchAttackingSide(EAttackingSide eAttackingSide)
        {
            SwitchedAtackingSide?.Invoke(eAttackingSide);
        }

        private void OnStepFinish(EAttackingSide eAttackingSide)
        {
            if (eAttackingSide == EAttackingSide.Right)
            {
                SwitchAttackingSide(EAttackingSide.Left);
                _steps.isRightStep = true;
            }
            else
            {
                SwitchAttackingSide(EAttackingSide.Right);
                _steps.isLeftStep = true;
            }

            if (_steps is { isLeftStep: true, isRightStep: true })
            {
                NextRound();
            }
        }

        private void NextRound()
        {
            CurrentRound++;
            UpdateRound?.Invoke(CurrentRound);
            ResetSteps();
            StartRound();
            _buffManager.UpdateRound(CurrentRound);
        }

        private void OnRespawnUnits()
        {
            _windowService.Open(WindowType.Battle);
            _windowService.Open(WindowType.Buffs);
            ResetSteps();
            ResetRounds();
            SwitchAttackingSide(EAttackingSide.Left);
        }

        private void ResetRounds()
        {
            CurrentRound = 0;
            OnResetRounds?.Invoke();
        }

        private void ResetSteps()
        {
            _steps.isRightStep = false;
            _steps.isLeftStep = false;
        }
    }
}