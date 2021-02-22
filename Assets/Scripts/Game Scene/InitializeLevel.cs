using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private Transform _playerSpawn = null;
    [SerializeField] private GamePlayerManager _playerManager = null;
    [SerializeField] private CameraSplitter _cameraSplitter = null;


    private void Awake()
    {

        var playerConfigs = PlayerConfigurationManager.get.playerConfigurations;
        Debug.Log("configs size " + playerConfigs.Count);
        for (int i = 0; i < playerConfigs.Count; i++)
        {
            var position = _playerSpawn.position;
            position.x += playerConfigs[i].index;

            var player = Instantiate(_playerPrefab, position, _playerSpawn.rotation, _playerManager.transform);
            
            if (playerConfigs[i].isRemotePlayer)
            {
                player.GetComponentInChildren<Camera>().enabled = false;
            }
            else
                _cameraSplitter.addCameras(player.GetComponent<PlayerCameraAndUI>());

            player.GetComponent<GameInputHandler>().initPlayer(playerConfigs[i]);
            Debug.Log("isremote? " + playerConfigs[i].isRemotePlayer);



            _playerManager.players.Add(player);
        }
    }
}
