using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<Quest> childQuests = new List<Quest>();
    public bool completed = false;
    public int numChildQuestsCompleted = 0;
    // Update is called once per frame
    void Update()
    {
        QuestUpdate();
        bool temp = childQuests.Count > 0 ? true : false;
        numChildQuestsCompleted = 0;
        foreach (var quest in childQuests)
        {
            if (!quest.completed)
                temp = false;
            else
                numChildQuestsCompleted++;
        }
        if (!completed)
            completed = temp;
    }

    protected virtual void QuestUpdate()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            completed = true;
    }
}
