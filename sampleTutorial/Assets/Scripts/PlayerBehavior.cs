using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    void FixedUpdate()
    {

    }
}

// public class PlayerBehavior : MonoBehaviour
// {
//     int gamecounter = 0;

//     //serializefield lets you see the atribute in the editor/inspector(public can do the same job)
//     [SerializeField] private Transform groundChecktransform;
//     private float horizontalInput;
//     private float verticalInput;
//     private bool jumpKeyWasPressed;
//     public Rigidbody rigidBody { get; private set; }
//     private Vector3 startPosition;
//     [SerializeField] private LayerMask playerMask;

//     private int superJump = 0;
//     private float superJumpMultiplier;

//     // Start is called before the first frame update
//     void Start()
//     {
//         rigidBody = GetComponent<Rigidbody>();
//         startPosition = rigidBody.position;
//     }


//     // Update is called once per frame
//     void Update()
//     {

//         //verification of the jump key
//         if (Input.GetKeyDown(KeyCode.Space))
//             jumpKeyWasPressed = true;


//         //getting horizontal and vertical directions
//         horizontalInput = Input.GetAxis("Horizontal") * 1.8f;
//         verticalInput = Input.GetAxis("Vertical") * 1.8f;

//         //reseting if dropped
//         if (rigidBody.position.y < -20)
//             rigidBody.position = startPosition;
//     }


//     // updates with an alpha of the phisics instead of pear frame
//     void FixedUpdate()
//     {
//         //this makes the horizontal moviment
//         rigidBody.velocity = new Vector3(horizontalInput, rigidBody.velocity.y, verticalInput);

//         if (Physics.OverlapSphere(groundChecktransform.position, 0.1f, playerMask).Length == 0)
//         {
//             if (superJump > 0)
//                 superJump--;
//             else
//                 return;
//         }

//         if (jumpKeyWasPressed)
//         {

//             superJumpMultiplier = 1;
//             if (superJump > 0)
//             {
//                 superJumpMultiplier = 1.2f;
//                 superJump--;
//             }

//             rigidBody.AddForce(Vector3.up * 7 * superJumpMultiplier, ForceMode.VelocityChange);
//             jumpKeyWasPressed = false;

//         }
//     }
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.layer == 7)
//         {
//             Destroy(other.gameObject);
//             ++gamecounter;
//             ++superJump;
//         }
//     }
// }
