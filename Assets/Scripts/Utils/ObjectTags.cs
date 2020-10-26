using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTags : MonoBehaviour {

    [SerializeField]
    private List<string> _tags = new List<string>();

    // @Cleanup: this may not be necessary, but I'm leaving available just in case.
    // Audit for this later and delete if not used.
    public List<string> tags { get { return _tags; } }

    public bool hasTag(string tag) {
        return _tags.Contains(tag);
    }

    public void addTag(string tag) { if(!hasTag(tag)) _tags.Add(tag); }

    // Also possible @Cleanup
    public void removeTag(string tag) { if(hasTag(tag)) _tags.Remove(tag); }


}
