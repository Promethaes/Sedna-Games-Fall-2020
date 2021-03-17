using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flingable : MonoBehaviour
{
    public float damage = 0;

    public bool hasLifetime = true;
    public float lifetime = 4.0f;
    float _lifetime = 4.0f;
    public Transform target;
    public bool lookAtTarget = true;

    // Start is called before the first frame update
    void Start()
    {
        _lifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform && lookAtTarget)
            transform.LookAt(target);
        if (!hasLifetime)
            return;
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0.0f)
        {
            _lifetime = lifetime;
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerBackend>().takeDamage(damage);
            if (Random.Range(0.0f, 1.0f) <= 0.3f)
                other.GetComponent<PlayerController>().slowed();
            gameObject.SetActive(false);
        }
    }
}
