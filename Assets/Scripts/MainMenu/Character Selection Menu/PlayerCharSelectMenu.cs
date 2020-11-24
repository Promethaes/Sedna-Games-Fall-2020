using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCharSelectMenu : MonoBehaviour
{

    public int charSelectIndex = 0;

     public List<PlayerTypeToGameObject> _characterPrefabs = null;

    [Header("Scene References")]
    [SerializeField] private GameObject _playerChoice = null;
    [SerializeField] private Image _confirmButton = null;
    [SerializeField] private TextMeshProUGUI _playerIndexText = null;
    [SerializeField] private TextMeshProUGUI _playerReadyText = null;

    public GameObject characterSelection
    {
        get
        {
            return _playerChoice;
        }
        private set
        {
            _playerChoice.GetComponent<MeshFilter>().mesh = value.GetComponentInChildren<MeshFilter>().sharedMesh;
            _playerChoice.GetComponent<MeshRenderer>().material = value.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        }
    }

    private int _playerIndex = 0;
    private float _ignoreInputTime = 1.5f;
    private bool _inputEnabled = true;
    private bool _allowScrolling = true;

    public void setPlayerIndex(int value)
    {
        _playerIndex = value;
        _playerIndexText.SetText("P" + (_playerIndex + 1).ToString());
        _ignoreInputTime = Time.time + _ignoreInputTime;
        _inputEnabled = false;
    }

    public void readyPlayer()
    {
        PlayerConfigurationManager.get.readyPlayer(_playerIndex);
        PlayerConfigurationManager.get.setPlayerCharacter(_playerIndex, _characterPrefabs[charSelectIndex]);
        _playerReadyText.gameObject.SetActive(true);
        _confirmButton.color = new Color(0.0f, 1.0f, 0.0f);
        _allowScrolling = false;
    }

    public void unreadyPlayer()
    {
        PlayerConfigurationManager.get.unreadyPlayer(_playerIndex);
        _playerReadyText.gameObject.SetActive(false);
        _confirmButton.color = new Color(1.0f, 1.0f, 1.0f);
        _allowScrolling = true;
    }

    public void startGame()
    {
        if (!_inputEnabled) return;
        PlayerConfigurationManager.get.allPlayersReady();
    }

    public void scrollSelectionForward()
    {
        if (_allowScrolling)
        {
            ++charSelectIndex;
            if (charSelectIndex >= _characterPrefabs.Count) charSelectIndex = 0;
            characterSelection = _characterPrefabs[charSelectIndex].prefab;
        }
    }

    public void scrollSelectionBackward()
    {
        if (_allowScrolling)
        {
            --charSelectIndex;
            if (charSelectIndex < 0) charSelectIndex = _characterPrefabs.Count - 1;
            characterSelection = _characterPrefabs[charSelectIndex].prefab;
        }
    }

    private void Awake()
    {
        if (_characterPrefabs.Count <= 0)
        {
            Debug.LogError("No character prefabs to choose from!");
            return;
        }

        if (_characterPrefabs[charSelectIndex] == null)
        {
            Debug.LogError("First character selection is null!");
            return;
        }

        characterSelection = _characterPrefabs[charSelectIndex].prefab;
        characterSelection.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.up) * characterSelection.transform.rotation;
    }
    void Update()
    {
        if (Time.time > _ignoreInputTime)
            _inputEnabled = true;
    }

}
