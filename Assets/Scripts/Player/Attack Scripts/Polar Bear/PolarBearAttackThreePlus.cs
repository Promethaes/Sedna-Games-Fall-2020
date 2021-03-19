using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBearAttackThreePlus : MonoBehaviour
{
    bool active = false;
    public GameObject crack;
    IEnumerator Attack()
    {
        Transform temp = transform.parent;
        transform.parent = null;
        yield return new WaitForSeconds(0.5f);
        crack.SetActive(true);
        yield return new WaitForSeconds(2.5f);
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
