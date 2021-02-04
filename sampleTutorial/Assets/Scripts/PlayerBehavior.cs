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
    //[SerializeField] private Transform groundChecktransform;

    [SerializeField] private Transform mainBody;

    // [SerializeField] private Rigidbody rigidBody;

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
        startPosition = mainBody.position;
    }


    // Update is called once per frame
    void Update()
    {
        //getting input
        jump = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal") * 1.8f;
        verticalInput = Input.GetAxis("Vertical") * 1.8f;
        ApplyMovement();
    }

    void ApplyMovement(){
        move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * speed * Time.deltaTime);

    
    }


    // updates with an alpha of the phisics instead of pear frame
    void FixedUpdate()
    {

        //reseting if dropped
        if (mainBody.position.y < -20)
            mainBody.position = startPosition;
        


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
