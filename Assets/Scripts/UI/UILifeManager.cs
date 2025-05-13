using System;
using UnityEngine;
using Utils;

namespace UI
{
    public class UILifeManager : MonoBehaviour, IUILifeManager
    {
        [SerializeField] private UILife[] _vetLife;
        [SerializeField] private Sprite _fullLife;
        [SerializeField] private Sprite _emptyLife;

        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public void SetQtdLife(int qtdLife)
        {
            int count = _vetLife.Length - qtdLife;
            count = count > _vetLife.Length ? _vetLife.Length : count;
      
            for (int i = 0; i < count; i++)
            {
                _vetLife[i].SetImage(_emptyLife);
            }
        }
        
        public void ResetLife()
        {
            for (int i = 0; i < _vetLife.Length; i++)
            {
                _vetLife[i].SetImage(_fullLife);
            }
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            ServiceLocator.UnregisterService<UILifeManager>();
        }
    }
}