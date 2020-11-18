using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerController))]
public class GameInputHandler : MonoBehaviour
{

    private GameObject _playerMesh = null;
    private PlayerConfiguration _playerConfig = null;
    private PlayerController _playerController = null;
    private UpdatedControls _controls = null;

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _controls = new UpdatedControls();
    }

    public void initPlayer(PlayerConfiguration config)
    {
        _playerConfig = config;
        _playerMesh = Instantiate(config.character.prefab, gameObject.transform);
        _playerController.playerType = config.character.type;

        _playerConfig.input.SwitchCurrentActionMap("Game");
        _playerConfig.input.onActionTriggered += onActionTriggered;
    }

    public void onActionTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == _controls.Game.Move.name) OnMove(ctx);
        if (ctx.action.name == _controls.Game.MouseInput.name) OnMouseInput(ctx);
        if (ctx.action.name == _controls.Game.Jump.name) OnJump(ctx);
        if (ctx.action.name == _controls.Game.Dash.name) OnDash(ctx);
        if (ctx.action.name == _controls.Game.Ability.name) OnAbility(ctx);
        if (ctx.action.name == _controls.Game.Attack.name) OnAttack(ctx);
        if (ctx.action.name == _controls.Game.Revive.name) OnRevive(ctx);
        
    }

    public void OnMove(CallbackContext ctx)
    {
        _playerController.moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnMouseInput(CallbackContext ctx)
    {
        _playerController.mouseInput = ctx.ReadValue<Vector2>();
    }
    public void OnJump(CallbackContext ctx)
    {
        _playerController.isJumping = ctx.ReadValue<float>();
    }
    public void OnDash(CallbackContext ctx)
    {
        _playerController.isDashing = ctx.ReadValue<float>();
    }

    public void OnAbility(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _playerController.useAbility = true;
        else
            _playerController.useAbility = false;
    }
    public void OnRevive(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _playerController.revive = true;
        else
            _playerController.revive = false;
    }

    public void OnAttack(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _playerController.attack = true;
        else
            _playerController.attack = false;

    }

}