using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBearAttackThreePlus : MonoBehaviour
{
    public GameObject[] crackPieces;
    IEnumerator Attack()
    {
        Transform temp = transform.parent;
        transform.parent = null;

        yield return new WaitForSeconds(0.8f);
        
        foreach (GameObject child in crackPieces)
            child.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        foreach (GameObject child in crackPieces)
            child.SetActive(false);

        transform.parent = temp;
        transform.localPosition = new Vector3(0.0f, -0.1f, 3.0f);
    }
    public void SlamAttack()
    {
        StartCoroutine(Attack());
    }
}
