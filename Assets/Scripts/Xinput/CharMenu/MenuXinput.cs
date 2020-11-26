using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuXinput : MonoBehaviour
{
    public XinputPlayerManager playerManager;

    XinputGamepad _gamepad;

    public bool joined = false;

    public int playerIndex = 0;

    public bool ready = false;

    float buttonPressCooldown = 0.5f;

    private PlayerCharSelectMenu playerCharSelectMenu;

    public Transform JoinPosition;

    public SceneChanger sceneChanger;

    public void SetMenuCallbacks()
    {
        _gamepad.SetEventCallback(Button.A, Join);
        _gamepad.SetEventCallback(Button.B, Leave);
        _gamepad.SetLeftStickCallback(ChangeSelection);
        _gamepad.SetEventCallback(Button.START,StartGame);
    }

    // Start is called before the first frame update
    void Start()
    {
        _gamepad = gameObject.GetComponent<XinputGamepad>();
        playerCharSelectMenu = gameObject.GetComponent<PlayerCharSelectMenu>();

        playerManager = FindObjectOfType<XinputPlayerManager>();

        SetMenuCallbacks();
    }

    // Update is called once per frame
    void Update()
    {
        buttonPressCooldown -= Time.deltaTime;
        if (buttonPressCooldown <= 0.0f)
            buttonPressCooldown = 0.0f;
    }

    void resetPressCooldown()
    {
        buttonPressCooldown = 0.5f;
    }

    void Join(ControllerStickValues values)
    {
        if (joined || buttonPressCooldown > 0.0f)
            return;
        playerManager.players.Add(gameObject);
        playerIndex = _gamepad.index;
        joined = true;
        gameObject.transform.position = JoinPosition.position;
        gameObject.transform.position = gameObject.transform.position - gameObject.transform.right * (playerIndex * 2.0f);
        _gamepad.SetEventCallback(Button.A, ReadyUp);
        resetPressCooldown();
    }

    void Leave(ControllerStickValues values)
    {
        if (!joined || buttonPressCooldown > 0.0f)
            return;
        playerManager.players.Remove(gameObject);
        joined = false;
        gameObject.transform.position = new Vector3(999.0f, 0.0f, 0.0f);
        _gamepad.SetEventCallback(Button.A, Join);
        resetPressCooldown();

    }

    void ReadyUp(ControllerStickValues values)
    {
        if (ready || buttonPressCooldown > 0.0f)
            return;
        ready = true;
        _gamepad.SetEventCallback(Button.B, Unready);
        resetPressCooldown();
    }

    void Unready(ControllerStickValues values)
    {
        if (!ready || buttonPressCooldown > 0.0f)
            return;
        ready = false;
        _gamepad.SetEventCallback(Button.B, Leave);
        resetPressCooldown();
    }

    void ChangeSelection(ControllerStickValues values)
    {
        if (buttonPressCooldown > 0.0f)
            return;

        if (values.leftStick.x > 0.5f)
            playerCharSelectMenu.scrollSelectionForward();
        else if (values.leftStick.x < -0.5f)
            playerCharSelectMenu.scrollSelectionBackward();

        resetPressCooldown();
    }

    void StartGame(ControllerStickValues values)
    {
        if (buttonPressCooldown > 0.0f)
            return;
        resetPressCooldown();

        bool readyToStart = true;
        foreach (var player in playerManager.players)
        {
            if (!player.GetComponent<MenuXinput>().ready)
                readyToStart = false;
        }

        if (readyToStart && playerIndex == 0)
            sceneChanger.changeScene(2);
    }



}
