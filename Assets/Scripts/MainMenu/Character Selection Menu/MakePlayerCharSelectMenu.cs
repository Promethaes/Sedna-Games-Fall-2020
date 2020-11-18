using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class MakePlayerCharSelectMenu : MonoBehaviour {

    public GameObject playerSetupMenu;
    public PlayerInput playerInput;

    private void Awake() {
        var taggedObjects = GameObject.FindObjectsOfType<ObjectTags>();
        GameObject rootMenu = null;
        foreach(var tagged in taggedObjects) {
            if(tagged.hasTag("LayoutGroup")) {
                rootMenu = tagged.gameObject;
                break;
            }
        }

        if(rootMenu == null) {
            Logger.Log("Could not make player character select menu: Failed to find LayoutGroup!");
            return;
        }

        var menu = Instantiate(playerSetupMenu, rootMenu.transform);

        playerInput.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
        menu.GetComponent<PlayerCharSelectMenu>().setPlayerIndex(playerInput.playerIndex);
        menu.GetComponent<CharSelectInputHandler>().initPlayerMenu(playerInput);

    }

}