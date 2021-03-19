using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerCharSelectMenu))]
public class CharSelectInputHandler : MonoBehaviour {
    [SerializeField] private float _selectionCooldownTime = 0.25f;
    [SerializeField] private float readyWait = 0.5f;

    private PlayerConfiguration _playerConfig;
    private UpdatedControls _controls;
    private PlayerCharSelectMenu _menu;
    private Vector2 _mouseInput;

    private float _selectionCooldown = 0.25f;
    private bool _selectAvailable = false;

    private void Awake() {
        _menu = gameObject.GetComponent<PlayerCharSelectMenu>();
        _controls = new UpdatedControls();
    }

    private void Update() {
        if(_selectAvailable) {
            Action resetCooldown = () => {
                _selectAvailable = false;
                _selectionCooldown = _selectionCooldownTime;
            };

            if(_mouseInput.x >= 0.5f) {
                _menu.scrollSelectionForward();
                resetCooldown();
            }
            else if(_mouseInput.x <= -0.5f) {
                _menu.scrollSelectionBackward();
                resetCooldown();
            }
        }
        else {
            _selectionCooldown -= Time.deltaTime;
            if(_selectionCooldown <= 0.0f) {
                _selectAvailable = true;
            }
        }
        if (readyWait > 0.0f)
        {
            readyWait -= Time.deltaTime;
        }
    }

    public void initPlayerMenu(PlayerInput playerInput) {
        _playerConfig = PlayerConfigurationManager.get.playerConfigurations.Find(p => p.index == playerInput.playerIndex);
        if(_playerConfig == null) {
            Logger.Error("Failed to get new PlayerConfiguration when joining!");
            return;
        }

        if(_playerConfig.input == null) {
            //Logger.Error("No PlayerInput attached to PlayerConfig!");
            return;
        }

        _playerConfig.input.SwitchCurrentActionMap("CharSelect");
        _playerConfig.input.onActionTriggered += onActionTriggered;
    }

    private void onActionTriggered(CallbackContext ctx) {
        if(ctx.action.name == _controls.CharSelect.Confirm.name) onConfirm(ctx);
        if(ctx.action.name == _controls.CharSelect.Unconfirm.name) onUnconfirm(ctx);
        if(ctx.action.name == _controls.CharSelect.StartGame.name) onStartGame(ctx);
        if(ctx.action.name == _controls.CharSelect.MouseMove.name) onMouseMove(ctx);
    }

    public void onConfirm(CallbackContext ctx) {
        float value = ctx.ReadValue<float>();
        if(value >= 0.5f&&readyWait<=0.0f) {
            _menu.readyPlayer();
            readyWait = 0.25f;
        }
    }

    public void onUnconfirm(CallbackContext ctx) {
        float value = ctx.ReadValue<float>();
        if(value >= 0.5f & readyWait <= 0.0f) {
            _menu.unreadyPlayer();
            readyWait = 0.25f;
        }
    }

    public void onStartGame(CallbackContext ctx) {
        float value = ctx.ReadValue<float>();
        if(value >= 0.5f) {
            _menu.startGame();
        }
    }

    public void onMouseMove(CallbackContext ctx) {
        _mouseInput = ctx.ReadValue<Vector2>();
    }
}