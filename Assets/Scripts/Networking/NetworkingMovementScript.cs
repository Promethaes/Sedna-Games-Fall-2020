using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetworkingMovementScript : MonoBehaviour
{

    Vector2 moveVec = new Vector2();
    Rigidbody rigidbody = null;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity + new Vector3(moveVec.x,0,moveVec.y);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveVec = ctx.ReadValue<Vector2>();
    }
}
