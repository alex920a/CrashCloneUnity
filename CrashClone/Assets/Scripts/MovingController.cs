using UnityEngine;
using System.Collections;

public class MovingController : MonoBehaviour {

    //controller for crash movements

    private float speed = 10f;
    private float jumpSpeed = 5f;
    private float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    private CharacterController movementsController;

    void Start()
    {
        movementsController = gameObject.GetComponent<CharacterController>();

        if(movementsController == null)
        {
            Debug.LogError("Cannot find CharacterController!");
        }
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKey(KeyCode.W))
        //    gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
        if(movementsController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        movementsController.Move(moveDirection * Time.deltaTime);
        
	}
}
