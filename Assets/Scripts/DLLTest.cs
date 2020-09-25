using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestDLL;

public class DLLTest : MonoBehaviour
{
    void Start()
    {
        DLLClass test = new DLLClass();
        int rand = test.GetRandomInt();
        print("Random int is: " + rand);
    }
}
