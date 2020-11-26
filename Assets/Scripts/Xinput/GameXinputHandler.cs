using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameXinputHandler : MonoBehaviour
{
    private GameObject _playerMesh = null;
    private PlayerConfiguration _playerConfig = null;
    private PlayerController _playerController = null;
    private XinputGamepad _gamepad = null;


    public void swapPlayer(int config)
    {
        var reference = _playerConfig.gameObject.GetComponentInChildren<MakePlayerCharSelectMenu>().playerSetupMenu.GetComponent<PlayerCharSelectMenu>()._characterPrefabs[config];
        _playerMesh.GetComponentInChildren<MeshFilter>().mesh = reference.prefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        _playerMesh.GetComponentInChildren<MeshRenderer>().material = reference.prefab.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        _playerConfig.character.type = reference.type;
        _playerController.playerType = reference.type;

        // Set UI image
        GameObject.Find("PlayerUIPanel(Clone)").GetComponent<PlayerHealthUI>().setCharacterImage(config);
    }

    public void initPlayer(PlayerConfiguration config)
    {
        _playerConfig = config;
        _playerMesh = Instantiate(config.character.prefab, gameObject.transform);
        _playerController.playerType = config.character.type;

        _gamepad = _playerConfig.gamepad;
        _gamepad.callbackList.Clear();
        SetAllCallbacks();
    }

    void SetAllCallbacks()
    {
        _gamepad.SetEventCallback(Button.A, OnJump);
        _gamepad.SetEventCallback(Button.B, OnDash);
        _gamepad.SetEventCallback(Button.X, OnAttacc);
        _gamepad.SetEventCallback(Button.SHOULDER_LEFT, OnAbility);
    }
    private void Awake()
    {
        if (!UseXinputScript.use)
        {
            this.enabled = false;
            return;
        }
        _playerController = gameObject.GetComponent<PlayerController>();
    }

    void OnMove(ControllerStickValues value)
    {
        _playerController.moveInput = value.leftStick;
    }

    void OnLook(ControllerStickValues value)
    {
        _playerController.mouseInput = value.rightStick;
    }

    void OnJump(ControllerStickValues value)
    {
        _playerController.isJumping = 1.0f;
    }

    void OnDash(ControllerStickValues value)
    {
        _playerController.isDashing = 1.0f;
    }

    void OnAbility(ControllerStickValues value)
    {
        _playerController.useAbility = true;
    }

    void OnRevive(ControllerStickValues value)
    {
        _playerController.revive = true;
    }

    void OnAttacc(ControllerStickValues value)
    {
        _playerController.attack = true;
    }

    private void Update()
    {
        _playerController.selectWheel = XinputManager.GetEventValue(_gamepad.index, (int)Button.LEFT_TRIGGER);

        _playerController.moveInput = _gamepad.leftStick;
        _playerController.mouseInput = _gamepad.rightStick;
    }

}
