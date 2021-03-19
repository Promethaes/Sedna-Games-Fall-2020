using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBearAttackThreePlus : MonoBehaviour
{
    bool active = false;
    public GameObject crack;
    public GameObject[] crackPieces;
    IEnumerator Attack()
    {
        Transform temp = transform.parent;
        transform.parent = null;

        yield return new WaitForSeconds(1.5f);
        
        foreach (GameObject child in crackPieces)
            child.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        foreach (GameObject child in crackPieces)
            child.SetActive(false);

        crack.SetActive(false);
        active = false;
        transform.parent = temp;
        transform.localPosition = new Vector3(0.0f, -0.1f, 3.0f);
    }
    void Update()
    {
        if (!active)
        {
            StartCoroutine(Attack());
            active = true;
        }
    }
}
