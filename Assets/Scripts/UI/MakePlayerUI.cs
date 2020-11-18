using UnityEngine;

public class MakePlayerUI : MonoBehaviour {
    public GameObject player;
    public GameObject UIPrefab;

    private void Awake() {
        GameObject rootCanvas = null;
        var taggedObjects = FindObjectsOfType<ObjectTags>();
        foreach(var tagged in taggedObjects) {
            if(tagged.hasTag("UICanvas")) {
                rootCanvas = tagged.gameObject;
                break;
            }
        }

        if(rootCanvas == null) {
            Logger.Error("Could not make player UI: Failed to find UICanvas!");
            return;
        }

        var ui = Instantiate(UIPrefab, rootCanvas.transform);
        ui.GetComponent<PlayerHealthUI>().backend = player.GetComponent<PlayerBackend>();
        player.GetComponent<PlayerCameraAndUI>().playerUIPanel = ui.GetComponent<RectTransform>();

        // gameObject.SetActive(false); // This is to avoid making more than one UI...don't know if this is @Robust or not
    }
}