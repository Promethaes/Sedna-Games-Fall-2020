using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SednaFmod
{

    public class SoundEventController : MonoBehaviour
    {
        FMOD.Studio.EventInstance instance;

        [FMODUnity.EventRef]
        public string eventRef;
        [SerializeField][Range(0.0f,1.0f)]
        float clearObstacle;
        // Start is called before the first frame update
        void Start()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(eventRef);
            instance.start();
        }

        // Update is called once per frame
        void Update()
        {
            instance.setParameterByName("clearObstacle",clearObstacle);
        }
    }

}