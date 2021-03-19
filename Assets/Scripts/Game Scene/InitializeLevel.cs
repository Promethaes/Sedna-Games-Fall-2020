﻿using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private Transform _playerSpawn = null;
    [SerializeField] private GamePlayerManager _playerManager = null;
    [SerializeField] private CameraSplitter _cameraSplitter = null;


    private void Awake()
    {
        var playerConfigs = PlayerConfigurationManager.get.playerConfigurations;
        for (int i = 0; i < playerConfigs.Count; i++)
        {
            var position = _playerSpawn.position;
            position.x += playerConfigs[i].index;

            var player = Instantiate(_playerPrefab, position, _playerSpawn.rotation, _playerManager.transform);

            if (playerConfigs[i].isRemotePlayer)
            {
                player.GetComponentInChildren<Camera>().enabled = false;
                player.GetComponent<FMODUnity.StudioListener>().enabled = false;
                player.name = "REMOTE";
                player.GetComponent<PlayerController>().userName = playerConfigs[i].userName;
                player.GetComponent<PlayerController>().remotePlayer = true;
            }
            else
            {
                _cameraSplitter.addCameras(player.GetComponent<PlayerCameraAndUI>());
                //NewCameraScript._player = player;
                //player.GetComponentInChildren<Camera>().enabled = false;
            }

            player.GetComponent<GameInputHandler>().initPlayer(playerConfigs[i]);

            if (!playerConfigs[i].isRemotePlayer)
                //CameraObject.player = player.GetComponentInChildren<SkinnedMeshRenderer>();
                CameraObject.ChangeSkinnedMesh(player.GetComponentInChildren<SkinnedMeshRenderer>());
            _playerManager.players.Add(player);
        }
    }
}
