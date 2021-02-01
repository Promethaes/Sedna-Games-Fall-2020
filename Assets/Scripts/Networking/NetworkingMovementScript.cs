using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkingMovementScript : MonoBehaviour
{

    Vector2 moveVec = new Vector2();
    Rigidbody rigidbody = null;

    public bool send = false;

    public NetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    float timer = 0.33f;
    float mTimer = 0.33f;
    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity + new Vector3(moveVec.x, 0, moveVec.y);
        timer -= Time.fixedDeltaTime;
        if (send && timer <= 0.0f){
            timer = mTimer;
            manager.Send("cli " + (manager.clientNum + 1).ToString() + " plr pos " +
            gameObject.transform.position.x.ToString() + " " + gameObject.transform.position.y.ToString() + " " + gameObject.transform.position.z.ToString());
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveVec = ctx.ReadValue<Vector2>();
    }
}
