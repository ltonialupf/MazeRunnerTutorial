using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private Action<InputAction.CallbackContext> OnActionKeyPerformed;
    private Action<InputAction.CallbackContext> OnActionKeyCancelled;

    private Vector3 _playerInitialPos;
    private CharacterController _charController;
    private int _qtdLife = 3;
    
    private UILifeManager _uiLifeManager;

    private void Start()
    {
        OnActionKeyPerformed = _ => ActionKey(true);
        OnActionKeyCancelled = _ => ActionKey(false);

        _playerInput.actions["ActionKey"].performed += OnActionKeyPerformed;
        _playerInput.actions["ActionKey"].canceled += OnActionKeyCancelled;
        
        _playerInitialPos = transform.position;
        
        //Exemplo de duas formas de getComponent, utilizando o Try é mais otimizado e
        //ele retorna true ou false para saber se conseguiu pegar o componente
        TryGetComponent(out _charController);
        //ou
        //_charController = GetComponent<CharacterController>();

        _uiLifeManager = ServiceLocator.GetService<UILifeManager>();
    }

    private void ActionKey(bool pressedKey)
    {
        Debug.Log($"ActionKey = {pressedKey}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Debug.Log($"OnTriggerEnter = {other.name}");
            transform.SetParent(other.transform);
        }
        else if (other.CompareTag("Dead"))
        {
            Debug.Log($"OnTriggerEnter = {other.name}");
            _charController.enabled = false;
            transform.position = _playerInitialPos;
            _charController.enabled = true;
            
            TakeHit();
        }
    }

    private void TakeHit()
    {
        _qtdLife = _qtdLife > 0 ? _qtdLife - 1 : 0;
        _uiLifeManager.SetQtdLife(_qtdLife);

        if (_qtdLife == 0)
        {
            Debug.Log("Game Over");
            //TODO - Mostrar tela ou ir para cena de fim de jogo

            var activeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(activeScene);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            Debug.Log($"OnTriggerExit = {other.name}");
            transform.SetParent(null);
        }
    }

    private void OnDestroy()
    {
        _playerInput.actions["ActionKey"].performed -= OnActionKeyPerformed;
        _playerInput.actions["ActionKey"].canceled -= OnActionKeyCancelled;
    }
}