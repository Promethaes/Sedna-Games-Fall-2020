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

    // Start is called before the first frame update
    void Start()
    {
        _lifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
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
            gameObject.SetActive(false);
        }
    }
}
