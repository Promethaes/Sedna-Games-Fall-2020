using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomManager : MonoBehaviour
{
   public int seed = 0;

   void Start(){
       UnityEngine.Random.InitState(seed);
       Debug.Log(UnityEngine.Random.Range(0.0f,1.0f));
   }
}
