using UnityEngine;

public class InitializeLevel : MonoBehaviour {
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private Transform _playerSpawn = null;
    [SerializeField] private GamePlayerManager _playerManager = null;
    [SerializeField] private CameraSplitter _cameraSplitter = null;

    private void Awake() {
        var playerConfigs = PlayerConfigurationManager.get.playerConfigurations.ToArray();

        for(int i = 0; i < playerConfigs.Length; i++) {


            var position = _playerSpawn.position;
            position.x += playerConfigs[i].index;

            var player = Instantiate(_playerPrefab, position, _playerSpawn.rotation, _playerManager.transform);
            player.GetComponent<GameInputHandler>().initPlayer(playerConfigs[i]);
            _cameraSplitter.addCameras(player.GetComponent<PlayerCameraAndUI>());

            _playerManager.players.Add(player);
        }
    }
}
