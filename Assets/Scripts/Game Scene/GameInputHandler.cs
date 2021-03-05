using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerController))]
public class GameInputHandler : MonoBehaviour
{

    private GameObject _playerMesh = null;
    private PlayerConfiguration _playerConfig = null;
    private PlayerController _playerController = null;
    private UpdatedControls _controls = null;

    public List<PlayerTypeToGameObject> _playerPrefabs = null;
    public Animator _animator;

    private void Awake()
    {
        if (UseXinputScript.use)
        {
            this.enabled = false;
            return;
        }

        _playerController = gameObject.GetComponent<PlayerController>();
        _controls = new UpdatedControls();

        var reference = GameObject.Find("PlayerConfig(Clone)").GetComponentInChildren<MakePlayerCharSelectMenu>().playerSetupMenu.GetComponent<PlayerCharSelectMenu>()._characterPrefabs;

        foreach (var p in reference)
        {
            var temp = GameObject.Instantiate(p.prefab, gameObject.transform);
            temp.SetActive(false);
            var pType = new PlayerTypeToGameObject();
            pType.prefab = temp;
            pType.type = p.type;
            _playerPrefabs.Add(pType);
        }
    }

    public void initPlayer(PlayerConfiguration config)
    {
        _playerConfig = config;
        _playerMesh = _playerPrefabs[config.index].prefab;
        _playerMesh.SetActive(true);
        _playerController.playerType = config.character.type;
        _animator = _playerMesh.GetComponentInChildren<Animator>();

        if (_playerConfig.input != null)
        {
            _playerConfig.input.SwitchCurrentActionMap("Game");
            _playerConfig.input.onActionTriggered += onActionTriggered;
        }
    }

    public void swapPlayer(int config)
    {
        _playerMesh.SetActive(false);
        var temp = _playerMesh.transform.rotation;
        _playerMesh = _playerPrefabs[config].prefab;
        _playerMesh.transform.rotation = temp;
        _playerMesh.SetActive(true);
        _playerConfig.character.type = _playerPrefabs[config].type;
        _playerController.playerType = _playerPrefabs[config].type;
        _animator = _playerMesh.GetComponentInChildren<Animator>();

        // Set UI image
        GameObject.Find("PlayerUIPanel(Clone)").GetComponent<PlayerHealthUI>().setCharacterImage(config);
    }

    public void onActionTriggered(CallbackContext ctx)
    {
        if (ctx.action.name == _controls.Game.Move.name) OnMove(ctx);
        if (ctx.action.name == _controls.Game.MouseInput.name) OnMouseInput(ctx);
        if (ctx.action.name == _controls.Game.Jump.name) OnJump(ctx);
        if (ctx.action.name == _controls.Game.Dash.name) OnDash(ctx);
        if (ctx.action.name == _controls.Game.Ability.name) OnAbility(ctx);
        if (ctx.action.name == _controls.Game.CombatAbility.name) OnCombatAbility(ctx);
        if (ctx.action.name == _controls.Game.Attack.name) OnAttack(ctx);
        if (ctx.action.name == _controls.Game.Toggle.name) OnToggle(ctx);
        if (ctx.action.name == _controls.Game.Revive.name) OnRevive(ctx);
        if (ctx.action.name == _controls.Game.Select.name) OnSelect(ctx);

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
        float temp = ctx.ReadValue<float>();
        _playerController.isJumping = temp >= 0.5f;
    }
    public void OnDash(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.isDashing = temp >= 0.5f;
    }

    public void OnAbility(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.useAbility = temp >= 0.5f;
    }
    public void OnCombatAbility(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.useCombatAbility = temp >= 0.5f;
    }
    public void OnRevive(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.revive = temp >= 0.5f;
    }

    public void OnAttack(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.attack = temp >= 0.5f;
    }
    public void OnToggle(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.toggle = temp >= 0.5f;
    }

    public void OnSelect(CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();
        _playerController.selectWheel = temp >= 0.5f;
    }

}