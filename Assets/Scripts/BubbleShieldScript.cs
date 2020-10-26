using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShieldScript : MonoBehaviour
{

    public GameObject bubble;
    public GameObject[] blockades;
    public float cooldown = 1.0f;
    public float duration = 1.0f;
    float _pvtDuration = 0.0f;
    float _pvtCooldown = 0.0f;
    bool _casting = false;

    // Start is called before the first frame update
    void Start()
    {
        _pvtCooldown = cooldown;
        _pvtDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        _pvtCooldown -= Time.deltaTime;

        if (_pvtCooldown <= 0.0f)
            _pvtCooldown = 0.0f;

        if (_casting)
        {
            _pvtDuration -= Time.deltaTime;
            if (_pvtDuration <= 0.0f)
                _ResetCooldownAndDuration();
        }

    }

    void _ResetCooldownAndDuration()
    {
        _pvtCooldown = cooldown;
        _pvtDuration = duration;
        _casting = false;
        bubble.SetActive(false);
    }

    public void AttemptToCast()
    {
        if (_pvtCooldown != 0.0f)
            return;

        _casting = true;
        bubble.SetActive(true);
        for (int i = 0;i<blockades.Length;i++)
            if (blockades[i] != null)
                blockades[i].SetActive(false);
    }
}
