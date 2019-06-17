using UnityEngine;
using System.Collections;

public class MovingController : MonoBehaviour {

    //   //controller for crash movements

    //   private float speed = 10f;
    //   private float jumpSpeed = 5f;
    //   private float gravity = 20.0f;
    //   private Vector3 moveDirection = Vector3.zero;

    //   private CharacterController movementsController;

    //   void Start()
    //   {
    //       movementsController = gameObject.GetComponent<CharacterController>();

    //       if(movementsController == null)
    //       {
    //           Debug.LogError("Cannot find CharacterController!");
    //       }
    //   }

    //// Update is called once per frame
    //void Update () {

    //       //if (Input.GetKey(KeyCode.W))
    //       //    gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
    //       if(movementsController.isGrounded)
    //       {
    //           moveDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
    //           //moveDirection = transform.TransformDirection(moveDirection);
    //           moveDirection *= speed;

    //           if (Input.GetKeyDown(KeyCode.Space))
    //               moveDirection.y = jumpSpeed;
    //       }

    //       moveDirection.y -= gravity * Time.deltaTime;
    //       movementsController.Move(moveDirection * Time.deltaTime);

    //}


    static Animator anim;
    public float speed = 10.0F;
    public float rotationSpeed = 100F;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if(Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isWalking");
        }


        if(translation != 0.0f)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}
