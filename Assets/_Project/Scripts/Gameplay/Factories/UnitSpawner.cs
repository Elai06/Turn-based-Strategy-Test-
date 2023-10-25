using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Gameplay.Configs;
using _Project.Scripts.Gameplay.Units;
using _Project.Scripts.Infrastructure.StaticData;
using _Project.Scripts.Infrastructure.Windows;
using Infrastructure.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Factories
{
    public class UnitSpawner : MonoBehaviour
    {
        public event Action OnRespawnUnits;

        private UnitsConfig _unitsConfig;
        private IWindowService _windowService;

        public List<UnitBase> LeftUnits { get; } = new();
        public List<UnitBase> RightUnits { get; } = new();

        [SerializeField] private Transform _playerPosition;
        [SerializeField] private Transform _enemyPosition;

        [Inject]
        public void Construct(GameStaticData gameStaticData, IWindowService windowService)
        {
            _unitsConfig = gameStaticData.GetUnitsConfig();
            _windowService = windowService;
        }

        public void Initialize()
        {
            SpawnUnits(_unitsConfig.LeftUnit, _playerPosition, LeftUnits);
            SpawnUnits(_unitsConfig.RightUnit, _enemyPosition, RightUnits);
        }

        private void SpawnUnits(UnitConfigData data, Transform position, List<UnitBase> spawnedUnits)
        {
            var unit = Instantiate(data.unitBase, position);
            unit.SetAttributes(data.ParametersData, data.EUnitType);
            spawnedUnits.Add(unit);
            unit.OnDied += () => OnDiedUnit(spawnedUnits);
        }

        private void ClearUnits(List<UnitBase> units)
        {
            foreach (var unit in units)
            {
                Destroy(unit.gameObject);
            }

            units.Clear();
        }

        private void OnDiedUnit(List<UnitBase> unitBases)
        {
            if (unitBases.Any(unit => !unit.IsDied))
            {
                return;
            }

            StartCoroutine(RespawnUnits());
        }

        private IEnumerator RespawnUnits()
        {
            ClearUnits(LeftUnits);
            ClearUnits(RightUnits);
            _windowService.Close(WindowType.Battle);
            _windowService.Close(WindowType.Buffs);

            yield return new WaitForSecondsRealtime(1);

            Initialize();
            OnRespawnUnits?.Invoke();
        }

        public void Restart()
        {
            StartCoroutine(RespawnUnits());
        }
    }
}