using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class PlayerBehavior : MonoBehaviour
// {
//     void Start()
//     {

//     }
//     void Update()
//     {

//     }
//     void FixedUpdate()
//     {

//     }
// }

public class PlayerBehavior : MonoBehaviour
{
    int gamecounter = 0;

    public CharacterController controller;
    Vector3 move;
    //serializefield lets you see the atribute in the editor/inspector(public can do the same job)
    [SerializeField] private Transform groundChecktransform;

    [SerializeField] private Rigidbody rigidBody;

    //input variables
    private float horizontalInput, verticalInput, jump;

    //position for the player when reseting
    private Vector3 startPosition;

    //player layer for collision tests
    [SerializeField] private LayerMask playerMask;


    //volcidade do caracter a andar
    float speed = 12f;


    private int superJump = 0;
    private float superJumpMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = rigidBody.position;
    }


    // Update is called once per frame
    void Update()
    {
        //getting input
        jump = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal") * 1.8f;
        verticalInput = Input.GetAxis("Vertical") * 1.8f;
    }



    // updates with an alpha of the phisics instead of pear frame
    void FixedUpdate()
    {

        //reseting if dropped
        if (rigidBody.position.y < -20)
            rigidBody.position = startPosition;

        //this makes the horizontal moviment
        move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * speed * Time.deltaTime);

        //verify if its touching the ground for the jump proprieties
        if (Physics.OverlapSphere(groundChecktransform.position, 0.1f, playerMask).Length == 0)
        {
            if (superJump > 0)
                superJump--;
            else
                return;
        }

        if (jump > 0.1f)
        {
            System.Console.WriteLine(jump);
            superJumpMultiplier = 1;
            if (superJump > 0)
            {
                superJumpMultiplier = 1.2f;
                superJump--;
            }

            rigidBody.AddForce(Vector3.up * 7 * superJumpMultiplier , ForceMode.VelocityChange);
            jump = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            ++gamecounter;
            ++superJump;
        }
    }
}
