using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XinputPlayerManager : MonoBehaviour
{
    public XinputManager inputManager;
    public List<PlayerConfiguration> players { get; private set; } = new List<PlayerConfiguration>();

    public static XinputPlayerManager get { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (get != null)
        {
            Logger.Error("Attempted to create new instance of XinputPlayerManager when one already exists!");
            return;
        }

        get = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
