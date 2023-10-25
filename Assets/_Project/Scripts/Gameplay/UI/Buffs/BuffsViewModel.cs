using System.Collections.Generic;
using System.Threading.Tasks;
using _Project.Scripts.Gameplay.Battle;
using _Project.Scripts.Gameplay.Buffs;
using _Project.Scripts.Gameplay.Enums;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Buffs
{
    public class BuffsViewModel : ViewModelBase<IBuffManager, BuffsView>
    {
        public BuffsViewModel(IBuffManager model, BuffsView view) : base(model, view)
        {
        }

        public override Task Show()
        {
            InitializeSubViews(Model.LeftSideBuffs, View.LeftSide);
            InitializeSubViews(Model.RightSideBuffs, View.RightSide);
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            base.Subscribe();
            Model.ActivatedBuff += OnActivateBuff;
            Model.DeactivatedBuff += OnDeactivatedBuff;
            Model.UpdateRounds += OnUpdateRounds;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            Model.ActivatedBuff -= OnActivateBuff;
            Model.DeactivatedBuff -= OnDeactivatedBuff;
            Model.UpdateRounds -= OnUpdateRounds;
        }

        private void OnUpdateRounds(EAttackingSide attackingSide, BuffBase buff)
        {
            UpdateBuff(attackingSide == EAttackingSide.Left ? View.LeftSide : View.RightSide, buff);
        }

        private void OnDeactivatedBuff(EAttackingSide attackingSide, BuffBase buffBase)
        {
            if (attackingSide == EAttackingSide.Left)
            {
                View.LeftSide.Remove(buffBase.EName.ToString());
            }
            else
            {
                View.RightSide.Remove(buffBase.EName.ToString());
            }
        }

        private void OnActivateBuff(EAttackingSide attackingSide, BuffBase buff)
        {
            CreateSubView(attackingSide == EAttackingSide.Left ? View.LeftSide : View.RightSide, buff);
        }

        private void InitializeSubViews(Dictionary<EBuffName, BuffBase> modelBuffs,
            BuffsSubViewContainer buffsSubViewContainer)
        {
            buffsSubViewContainer.CleanUp();
            
            foreach (var buff in modelBuffs.Values)
            {
                CreateSubView(buffsSubViewContainer, buff);
            }
        }

        private static void CreateSubView(BuffsSubViewContainer buffsSubViewContainer, BuffBase buff)
        {
            var buffViewData = new BuffSubViewData
            {
                BuffName = buff.EName,
                Rounds = buff.Round
            };

            buffsSubViewContainer.Add(buff.EName.ToString(), buffViewData);
        }

        private void UpdateBuff(BuffsSubViewContainer buffsSubViewContainer, BuffBase buff)
        {
            var buffViewData = new BuffSubViewData
            {
                BuffName = buff.EName,
                Rounds = buff.Round
            };

            buffsSubViewContainer.UpdateView(buffViewData, buff.EName.ToString());
        }
    }
}