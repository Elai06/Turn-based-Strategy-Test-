using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Buffs;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Battle
{
    public class BattleViewModelFactory : IViewModelFactory<BattleViewModel, BattleView, IBattleSystem>
    {
        private IBattleManager _battleManager;
        private IBuffManager _buffManager;

        public BattleViewModelFactory(IBattleManager battleManager, IBuffManager buffManager)
        {
            _battleManager = battleManager;
            _buffManager = buffManager;
        }   
        
        public BattleViewModel Create(IBattleSystem model, BattleView view)
        {
            return new BattleViewModel(model, view, _battleManager, _buffManager);
        }
    }
}