using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public List<GameObject> players;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
            players[i].SetActive(false);
    }


    /*
     * @brief: This function makes sure players are set to active/inactive based on the numPlayers
     * @param: numPlayers is the number of players
     * @return: void
     */
    public void ActivePlayerSetup(int numPlayers)
    {
        Debug.Assert(!(numPlayers > 4) || !(numPlayers < 1));

        for (int i = 0; i < numPlayers; i++)
            players[i].SetActive(true);

        int inactivePlayers = (4 - numPlayers);
        int j = 3;
        for (int i = 0; i < inactivePlayers; i++)
        {
            players[j].SetActive(false);
            j--;
        }

    }

}
