using System.Threading.Tasks;
using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Buffs;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Battle
{
    public class BattleViewModel : ViewModelBase<IBattleSystem, BattleView>
    {
        private const int MAX_BUFFS = 2;
        
        private IBattleManager _battleManager;
        private IBuffManager _buffManager;

        public BattleViewModel(IBattleSystem model, BattleView view, IBattleManager battleManager,
            IBuffManager buffManager) : base(model, view)
        {
            _battleManager = battleManager;
            _buffManager = buffManager;
        }

        public override Task Show()
        {
            ResetRounds();
            View.ShowBuffButton(EAttackingSide.Left, true);
            View.ShowBuffButton(EAttackingSide.Right, true);
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            base.Subscribe();

            View.OnAttacking += Model.Attack;
            View.OnUseBaff += Model.UseBuff;
            _battleManager.UpdateRound += OnRoundUpdate;
            _battleManager.SwitchedAtackingSide += OnSwitchedSide;
            _battleManager.OnResetRounds += ResetRounds;
            _buffManager.ActivatedBuff += OnActivatedBuff;
            _buffManager.DeactivatedBuff += OnActivatedBuff;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            View.OnAttacking -= Model.Attack;
            View.OnUseBaff -= Model.UseBuff;
            _battleManager.UpdateRound -= OnRoundUpdate;
            _battleManager.SwitchedAtackingSide -= OnSwitchedSide;
            _battleManager.OnResetRounds -= ResetRounds;
            _buffManager.ActivatedBuff -= OnActivatedBuff;
            _buffManager.DeactivatedBuff -= OnActivatedBuff;
        }

        private void OnActivatedBuff(EAttackingSide attackingSide, BuffBase buff)
        {
            var buffs = attackingSide == EAttackingSide.Left
                ? _buffManager.LeftSideBuffs : _buffManager.RightSideBuffs;

            View.ShowBuffButton(attackingSide, buffs.Count < MAX_BUFFS);
        }

        private void ResetRounds()
        {
            OnRoundUpdate(0);
        }

        private void OnRoundUpdate(int round)
        {
            View.SetRoundText(round);
        }

        private void OnSwitchedSide(EAttackingSide attackingSide)
        {
            View.ShowGameplayButtons(attackingSide);
        }
    }
}