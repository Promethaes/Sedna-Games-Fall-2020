using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredits : MonoBehaviour
{
    public float scrollSpeed = 2.0f;
    public float endYCoord = -1;
    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu2");
    }

    bool doneScroll = false;
    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= endYCoord && !doneScroll)
        {
            StartCoroutine("BackToMenu");
            doneScroll = true;
            return;
        }
        
        if (doneScroll)
            return;

        gameObject.transform.position += new Vector3(0.0f, scrollSpeed, 0.0f) * Time.deltaTime;
    }
}
