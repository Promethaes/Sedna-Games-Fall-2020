using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flies : MonoBehaviour
{
    public GameObject target;
    public float chaseSpeed = 2.0f;
    Rigidbody _rigid;

    bool shouldHit = false;

    public float maxTime = 1.0f;
    float _timer = 1.0f;

    public float damage = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody>();
        _timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        _rigid.velocity = (target.transform.position - transform.position).normalized * chaseSpeed;

        _timer -= Time.deltaTime;

        if (shouldHit && _timer <= 0.0f)
        {
            target.GetComponent<PlayerBackend>().hp -= damage;
            _timer = maxTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            shouldHit = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            shouldHit = false;
    }
}
