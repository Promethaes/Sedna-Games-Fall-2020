using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    GameObject player;
    float _speed = 5.0f;
    public Rigidbody _rigidbody;

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            _findPlayer();
        _Move();
    }
    void _findPlayer()
    {
        var temp = GameObject.Find("PlayerManager").GetComponent<GamePlayerManager>().players;
        GameObject target = temp[0];
        for (int i=0;i<temp.Count;i++)
        {
            if (Vector3.Distance(temp[i].transform.position, transform.position) <= Vector3.Distance(target.transform.position, transform.position))
                target = temp[i];
        }
        player = target;
    }
    void _Move()
    {
        Vector3 dir = Vector3.Normalize(player.transform.position - transform.position);
        _rigidbody.velocity = dir * _speed;
        if (Vector3.Distance(player.transform.position, transform.position) <= 0.15f)
        {
            player.GetComponentInChildren<PlayerBackend>().hp+=20.0f;
            gameObject.SetActive(false);
            player = null;
            HealthOrbManager.GetHealthOrbManager().setOrb(gameObject);
        }
    }
}
