using _Project.Scripts.Gameplay.Buffs;
using Infrastructure.Windows.MVVM;

namespace _Project.Scripts.Gameplay.UI.Buffs
{
    public class BuffsViewModelFactory : IViewModelFactory<BuffsViewModel, BuffsView, IBuffManager>
    {
        public BuffsViewModel Create(IBuffManager model, BuffsView view)
        {
            return new BuffsViewModel(model, view);
        }
    }
}