using Infrastructure.Windows.MVVM.SubView;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.UI.Buffs
{
    public class BuffSubView : SubView<BuffSubViewData>
    {
        [SerializeField] private TextMeshProUGUI _name;
        
        public override void Initialize(BuffSubViewData data)
        {
            _name.text = $"{data.BuffName} </b>[{data.Rounds}]";
        }
    }
}