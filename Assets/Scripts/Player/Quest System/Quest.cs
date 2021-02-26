using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour {
    public List<Quest> childQuests = new List<Quest>();
    public bool completed = false;
    public int numChildQuestsCompleted = 0;

    public TextMeshProUGUI questText;
    public TextMeshProUGUI questCounter;

    // Update is called once per frame
    void Update() {
        QuestUpdate();
        bool temp = childQuests.Count > 0 ? true : false;
        numChildQuestsCompleted = 0;

        foreach(var quest in childQuests) {
            if(!quest.completed) {
                temp = false;
            }
            else {
                numChildQuestsCompleted++;
            }
        }

        if(questCounter != null) {
            questCounter.text = numChildQuestsCompleted.ToString() + "/" + childQuests.Count.ToString();
        }

        if(!completed) {
            completed = temp;
        }
        else {
            if(questCounter != null) 
                questText.color = new Color(0, 1, 0);
        }
    }

    protected virtual void QuestUpdate() {

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            completed = true;
        }
    }
}
