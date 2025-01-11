using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

[CreateAssetMenu(fileName = "NewGameInputReader", menuName = "Scriptable Objects/GameInputReader")]
public class GameInputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
{
    // Player Actions
    public event Action<Vector2> MoveInputHasChanged;
    public event Action<Vector2, bool> LookInputHasChanged;
    public event Action JumpIsStarted;
    public event Action JumpIsCanceled;
    public event Action<bool> SprintIsTriggered;
    public event Action<bool> PauseIsTriggered;
    public event Action AttackIsPerformed;
    public event Action AttackIsCanceled;

    // UI Actions
    //...

    [SerializeField]
    private InputActionMap _defaultActionMap;

    [Header("Input Settings")]
    [SerializeField]
    private bool _invertXLookInput = false;
    [SerializeField]
    private bool _invertYLookInput = false;

    private GameInput _gameInput = default;
    private ReadOnlyArray<InputActionMap> _actionMaps;
    private InputActionMap _currentActionMap;

    // Debug Member Values
    private Vector2 _moveInput;
    private Vector2 _lookInput;

    // Properties
    public ReadOnlyArray<InputControlScheme> ControlSchemes { get => _gameInput.controlSchemes; }
    public bool LookXInputIsInverted { get => _invertXLookInput; private set => _invertXLookInput = value; }
    public bool LookYInputIsInverted { get => _invertYLookInput; private set => _invertYLookInput = value; }
    public Vector3 MoveDirection => new Vector3(_moveInput.x, 0, _moveInput.y);

    #region Unity MonoBehaviour Methods
    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _actionMaps = _gameInput.asset.actionMaps;

            _gameInput.Player.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);

            _defaultActionMap = _actionMaps[0];
        }

        EnableDefaultActionMap();
    }

    private void OnDisable()
    {
        DisableAllActionMaps();
    }
    #endregion

    #region Enable/Disable Action Maps
    public void EnableDefaultActionMap()
    {
        _defaultActionMap.Enable();
        _currentActionMap = _defaultActionMap;
    }

    public void SwitchCurrentActionMap(string actionMapName)
    {
        foreach (var actionMap in _actionMaps)
        {
            if (actionMap.name == actionMapName && _currentActionMap.name != actionMapName)
            {
                _currentActionMap.Disable();
                actionMap.Enable();
                _currentActionMap = actionMap;
                break;
            }
        }
        Debug.LogErrorFormat($"Cannot find action map '{actionMapName}' in '{_gameInput.asset.name}'.", this);
    }

    public void SwitchCurrentActionMap(Guid actionMapID)
    {
        foreach (var actionMap in _actionMaps)
        {
            if (actionMap.id == actionMapID && _currentActionMap.id != actionMapID)
            {
                _currentActionMap.Disable();
                actionMap.Enable();
                _currentActionMap = actionMap;
                break;
            }
        }
        Debug.LogErrorFormat($"Cannot find action map '{actionMapID}' in '{_gameInput.asset.name}'.", this);
    }

    public void DisableAllActionMaps()
    {
        foreach (var actionMap in _actionMaps)
            actionMap.Disable();
    }
    #endregion

    #region PlayerActionMap CallbackFunctions
    public void OnMove(InputAction.CallbackContext context)
    {
        if (MoveInputHasChanged is not null && context.phase == InputActionPhase.Performed)
        {
            _moveInput = context.ReadValue<Vector2>();
            MoveInputHasChanged.Invoke(_moveInput);
        }

        if (MoveInputHasChanged is not null && context.phase == InputActionPhase.Canceled)
        {
            _moveInput = Vector2.zero;
            MoveInputHasChanged.Invoke(_moveInput);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (LookInputHasChanged is not null && context.phase == InputActionPhase.Performed)
        {
            _lookInput = context.ReadValue<Vector2>();
            LookInputHasChanged.Invoke(_lookInput, IsDeviceMouse(context));
        }

        if (LookInputHasChanged is not null && context.phase == InputActionPhase.Canceled)
        {
            _lookInput = Vector2.zero;
            LookInputHasChanged.Invoke(_lookInput, IsDeviceMouse(context));
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (AttackIsPerformed is not null && context.phase == InputActionPhase.Performed)
            AttackIsPerformed.Invoke();
        if (AttackIsCanceled is not null && context.phase == InputActionPhase.Canceled)
            AttackIsCanceled.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpIsStarted is not null && context.phase == InputActionPhase.Started)
            JumpIsStarted.Invoke();

        if (JumpIsCanceled is not null && context.phase == InputActionPhase.Canceled)
            JumpIsCanceled.Invoke();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (SprintIsTriggered is not null && context.phase == InputActionPhase.Performed)
            SprintIsTriggered.Invoke(true);

        if (SprintIsTriggered is not null && context.phase == InputActionPhase.Canceled)
            SprintIsTriggered.Invoke(false);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (PauseIsTriggered is not null && context.phase == InputActionPhase.Performed)
            PauseIsTriggered.Invoke(true);

        if (PauseIsTriggered is not null && context.phase == InputActionPhase.Canceled)
            PauseIsTriggered.Invoke(false);
    }

    public void OnZoom(InputAction.CallbackContext context)
    {

    }
    #endregion

    #region UIActionMap CallbackFunctions
    public void OnNavigate(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    #endregion

    private bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";
}
