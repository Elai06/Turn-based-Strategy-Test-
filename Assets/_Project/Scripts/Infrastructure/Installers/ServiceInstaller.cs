using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Buffs;
using _Project.Scripts.Gameplay.Factories;
using _Project.Scripts.Gameplay.UI.Battle;
using _Project.Scripts.Gameplay.UI.Buffs;
using _Project.Scripts.Infrastructure.PersistenceProgress;
using _Project.Scripts.Infrastructure.SaveLoads;
using _Project.Scripts.Infrastructure.StateMachine;
using _Project.Scripts.Infrastructure.StateMachine.States;
using _Project.Scripts.Infrastructure.StaticData;
using Infrastructure.SaveLoads;
using Infrastructure.SceneManagement;
using Infrastructure.UnityBehaviours;
using Infrastructure.Windows;
using SirGames.Scripts.Infrastructure.StateMachine;
using SirGames.Scripts.Infrastructure.Windows;
using SirGames.Scripts.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineService _coroutineService;
        [SerializeField] private LayersContainer _layersContainer;
        [SerializeField] private GameStaticData _gameStaticData;
        [SerializeField] private UnitSpawner _unitSpawner;

        public override void InstallBindings()
        {
            BindViewModelFactory();
            BindGameStates();
            BindInfrastructureServices();
            BindModels();
        }

        private void BindViewModelFactory()
        {
            Container.BindInterfacesAndSelfTo<BattleViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuffsViewModelFactory>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ExitState>().AsSingle();
            Container.Bind<GameState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IProgressService>().To<PlayerProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();

            Container.Bind<LayersContainer>().FromInstance(_layersContainer).AsSingle();
            Container.Bind<GameStaticData>().FromInstance(_gameStaticData).AsSingle();
            Container.Bind<ICoroutineService>().FromInstance(_coroutineService).AsSingle();
            Container.Bind<UnitSpawner>().FromInstance(_unitSpawner).AsSingle();
        }

        private void BindModels()
        {
            Container.Bind<IBattleManager>().To<BattleManager>().AsSingle();
            Container.Bind<IBattleSystem>().To<BattleSystem>().AsSingle();
            Container.Bind<IBuffManager>().To<BuffManager>().AsSingle();
        }
    }
}