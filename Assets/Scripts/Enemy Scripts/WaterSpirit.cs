using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpirit : Enemy
{
    public WaterBomb projectile;
    public GameObject player;
    WaterBomb[] _waterBombs = new WaterBomb[3];

    // Time to reach player
    float _projectileSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        setHealth(80.0f);
        setCombo(15.0f, 15.0f, 25.0f, 1.5f, 1.5f, 3.0f, 10.0f, 10.0f, 10.0f);
        for (int i=0;i<3;i++)
        {
        _waterBombs[i] = Instantiate(projectile);
        _waterBombs[i].setDamage(10.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Test attack
        Combo();
        _animationDuration -= Time.deltaTime;
    }

    void Combo()
    {
        if (_animationDuration < 0.0f)
        {
            //TODO: Animation; lobbed water bombs, replace targetting method with Evyn's AI stuff
                _animationDuration = _animationDelay[_comboCounter];

                float distance = ((player.transform.position - new Vector3(0.0f, player.GetComponentInChildren<BoxCollider>().size.y, 0.0f) )- transform.position).magnitude;
                Vector3 direction = (player.transform.position - transform.position).normalized;
                float speed = distance/_projectileSpeed;
                Vector3 velocity = new Vector3(direction.x, 0.0f,direction.z) * speed;
                velocity.y = 9.8f*0.75f;

                _waterBombs[_comboCounter].gameObject.SetActive(true);
                _waterBombs[_comboCounter].GetComponent<Transform>().position = transform.position + new Vector3(0.0f, 2.0f, 0.0f);
                _waterBombs[_comboCounter].GetComponent<Rigidbody>().velocity = velocity;


                _comboCounter++;
            
            if (_comboCounter > 2)
                _comboCounter = 0;
        }
    }
}
