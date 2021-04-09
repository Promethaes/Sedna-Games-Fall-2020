using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerConfigurationManager : MonoBehaviour
{
    // @Cleanup: If we're gonna use Unity's Scene manager, scene changer doesn't need to exist here
    // [SerializeField] private SceneChanger _sceneChanger = null;

    private PlayerInputManager _manager = null;
    private List<PlayerConfiguration> _playerConfigs = null;
    public List<PlayerConfiguration> playerConfigurations { get { return _playerConfigs; } }

    public static PlayerConfigurationManager get { get; private set; }

    public CSNetworkManager networkManager;
    public GameObject _configPrefab;
    private void Awake()
    {
        if (get != null)
        {
            Logger.Error("Attempted to create new instance of PlayerConfigurationManager when one already exists!");
            return;
        }
        get = this;
        DontDestroyOnLoad(get);

        _manager = GetComponent<PlayerInputManager>();

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
            var p = new PlayerConfiguration(playerInput);
            _playerConfigs.Add(p);

            networkManager.AddNetworkedPlayer(p, true);
        }
    }

    public void handlePlayerLeft(PlayerInput playerInput)
    {
        if (_playerConfigs.Any(p => p.index == playerInput.playerIndex))
        {
            Logger.Log("Player {0} left", playerInput.playerIndex);
        }
    }

    public void allPlayersReady(bool tempDebugBool)
    {
        if(_playerConfigs.Count <= _manager.maxPlayerCount && _playerConfigs.All(p => p.isReady))
        {
            if(tempDebugBool)
                UnityEngine.SceneManagement.SceneManager.LoadScene("Arctic Level"); // This should be the game scene
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                // SceneManager.LoadScene("Game Scene"); // This should be the game scene

        }
    }
}
