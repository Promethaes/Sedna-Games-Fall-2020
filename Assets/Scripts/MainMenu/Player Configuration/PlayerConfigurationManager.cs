using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : MonoBehaviour
{

    [SerializeField] private SceneChanger _sceneChanger = null;

    private PlayerInputManager _manager = null;
    private XinputPlayerManager _xinputManager = null;
    private List<PlayerConfiguration> _playerConfigs = null;
    public List<PlayerConfiguration> playerConfigurations { get { return _playerConfigs; } }

    public static PlayerConfigurationManager get { get; private set; }
    public static bool usingXinput = false;

    private void Awake()
    {
        if (get != null)
        {
            Logger.Error("Attempted to create new instance of PlayerConfigurationManager when one already exists!");
            return;
        }

        get = this;
        DontDestroyOnLoad(get);

        if (!usingXinput)
            _manager = GetComponent<PlayerInputManager>();
        else
            _xinputManager = GetComponent<XinputPlayerManager>();

        _playerConfigs = new List<PlayerConfiguration>();
    }

    public void setPlayerCharacter(int index, PlayerTypeToGameObject character)
    {
        _playerConfigs[index].character = character;
    }

    public void readyPlayer(int index)
    {
        _playerConfigs[index].isReady = true;
    }

    public void unreadyPlayer(int index)
    {
        _playerConfigs[index].isReady = false;
    }

    public void handlePlayerJoined(PlayerInput playerInput)
    {
        if (!_playerConfigs.Any(p => p.index == playerInput.playerIndex))
        {
            Logger.Log("Player {0} joined", playerInput.playerIndex);
            playerInput.transform.parent = transform;
            _playerConfigs.Add(new PlayerConfiguration(playerInput));
        }
    }

    public void handlePlayerLeft(PlayerInput playerInput)
    {
        if (_playerConfigs.Any(p => p.index == playerInput.playerIndex))
        {
            Logger.Log("Player {0} left", playerInput.playerIndex);
        }
    }

    public void allPlayersReady()
    {
        if (_playerConfigs.Count <= _manager.maxPlayerCount && _playerConfigs.All(p => p.isReady))
        {
            _sceneChanger.changeScene(2); // This should be the game scene
        }
    }
}