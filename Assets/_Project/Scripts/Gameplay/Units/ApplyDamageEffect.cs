using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Units
{
    public class ApplyDamageEffect : MonoBehaviour
    {
        [SerializeField] private Material _material;

        private Color _preColor;
        private Tween _tween;

        private void OnEnable()
        {
            _preColor = _material.color;
        }

        private void OnDisable()
        {
            _material.color = _preColor;
        }

        public void Effect()
        {
            _tween?.Kill();
          _tween =  _material.DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => { _material.color = _preColor; });
        }
    }
}