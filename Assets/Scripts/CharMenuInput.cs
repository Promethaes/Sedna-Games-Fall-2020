using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharMenuInput : MonoBehaviour
{
    Vector2 _mouseInput;
    public bool confirm = false;
    bool _startButtonpressed = false;

    public List<GameObject> characterPrefabs;

    // @Refactor: could probably make this an enum
    public int playerType;// 1, 2, 3, 4
    public Image confirmImage;

    int _selectionIndex = 0;
    float _selectionCooldown = 0.0f;
    public float selectCooldown = 0.5f;

    public MenuPlayerManager menuPlayerManager;
    public float moveBy = 1.0f;

    public List<Image> selectionImages;

    public GameObject enableOnLoad;
    public GameObject disableOnLoad;

    public GameObject gameActionMap;

    // Start is called before the first frame update
    void Start()
    {
        confirm = false;

        gameObject.transform.position = gameObject.transform.position + new Vector3(moveBy, 0.0f, 0.0f) * menuPlayerManager.players.Count;
        
        for(int i = 0;  i < selectionImages.Count; i++)
            selectionImages[i].transform.position = selectionImages[i].transform.position + new Vector3(220.0f, 0.0f, 0.0f) * menuPlayerManager.players.Count;

        menuPlayerManager.players.Add(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        _selectionCooldown -= Time.deltaTime;

        if (_selectionCooldown <= 0.0f)
            _selectionCooldown = 0.0f;

        if (_mouseInput.x >= 0.5f && _selectionCooldown == 0.0f)
            _ScrollSelectionRight();
        else if (_mouseInput.x <= -0.5f && _selectionCooldown == 0.0f)
            _ScrollSelectionLeft();

        if (confirm && _selectionCooldown == 0.0f)
            confirmImage.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        else if (!confirm && _selectionCooldown == 0.0f)
            confirmImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    }

    void _ChangeCharacter(int index)
    {
        gameObject.GetComponent<MeshFilter>().mesh = characterPrefabs[index].GetComponent<MeshFilter>().sharedMesh;
        gameObject.GetComponent<MeshRenderer>().material = characterPrefabs[index].GetComponent<MeshRenderer>().sharedMaterial;
        gameObject.GetComponent<Rigidbody>().mass = characterPrefabs[index].GetComponent<Rigidbody>().mass;
        playerType = characterPrefabs[index].GetComponent<CharMenuInput>().playerType;
        _selectionCooldown = selectCooldown;
    }

    //https://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
    //why? why do you need to do this? C# is trash
    void _ScrollSelectionLeft()
    {
        _selectionIndex = (_selectionIndex - 1 + characterPrefabs.Count) % characterPrefabs.Count;
        _ChangeCharacter(_selectionIndex);
    }
    
    void _ScrollSelectionRight()
    {
        _selectionIndex = (_selectionIndex + 1) % characterPrefabs.Count;
        _ChangeCharacter(_selectionIndex);
    }

    public void OnMouseInput(InputAction.CallbackContext ctx)
    {
        _mouseInput = ctx.ReadValue<Vector2>();
    }

    public void OnConfirm(InputAction.CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            confirm = true;
        _ChangeCharacter(_selectionIndex);
    }

    public void OnUnConfirm(InputAction.CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            confirm = false;
    }

    public void OnStartButton(InputAction.CallbackContext ctx)
    {
        float temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _startButtonpressed = true;
        else
            _startButtonpressed = false;

        bool allConfirm = true;

        //make sure that we flag properly when the list is empty
        allConfirm = menuPlayerManager.players.Count == 0 ? false : true;

        for(int i = 0; i < menuPlayerManager.players.Count; i++)
            if (!menuPlayerManager.players[i].GetComponent<CharMenuInput>().confirm)
                allConfirm = false;

        if (allConfirm && _startButtonpressed)
            _FinalizeSelection();

    }

    //Save the player's character types in the player prefs to be accessed next scene, then load the game scene
    void _FinalizeSelection()
    {
        menuPlayerManager.PreservePlayers();

        var sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        sceneChanger.changeScene(2);

        this.enabled = false;
    }

    //This function is for when you wanna set up all the players for the game scene.
    public void Cleanup()
    {
        enableOnLoad.SetActive(true);
        disableOnLoad.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("Game");
    }


}
