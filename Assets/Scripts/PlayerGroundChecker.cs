using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    
    private Action<InputAction.CallbackContext> OnActionKeyPerformed;
    private Action<InputAction.CallbackContext> OnActionKeyCancelled;
    
    private void Start()
    {
        OnActionKeyPerformed = _ => ActionKey(true);
        OnActionKeyCancelled = _ => ActionKey(false);

        _playerInput.actions["ActionKey"].performed += OnActionKeyPerformed;
        _playerInput.actions["ActionKey"].canceled += OnActionKeyCancelled;
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