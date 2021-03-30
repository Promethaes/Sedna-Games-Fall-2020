using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Feedback",menuName = "Sedna Games Custom/Combat Feedback")]
public class CombatFeedback : ScriptableObject
{
    public Material flickerMaterial;
    public float flickerDuration;
}
