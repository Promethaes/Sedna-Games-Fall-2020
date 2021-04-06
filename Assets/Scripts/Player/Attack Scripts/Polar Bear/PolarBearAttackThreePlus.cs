using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PolarBearAttackThreePlus : MonoBehaviour
{
    public VisualEffect crack;
    IEnumerator Attack()
    {
        transform.parent = null;

        crack.Play();        
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(Attack());
    }
}
