using _Project.Scripts.Gameplay.Factories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.Gameplay
{
    public class RestartGameplayButton : MonoBehaviour
    {
        private Button _button;

        private UnitSpawner _unitSpawner;

        [Inject]
        private void Construct(UnitSpawner unitSpawner)
        {
            _unitSpawner = unitSpawner;
        }

        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
          _unitSpawner.Restart();  
        }
    }
}