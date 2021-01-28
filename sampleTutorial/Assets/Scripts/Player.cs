using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int gamecounter = 0;

    //serializefield lets you see the atribute in the editor/inspector(public can do the same job)
    [SerializeField] private Transform groundChecktransform;
    private float horizontalInput;
    private bool jumpKeyWasPressed;
    public Rigidbody rigidBody { get; private set; }
    private Vector3 startPosition;
    [SerializeField] private LayerMask playerMask;

    private int moneyHoney = 0;
    private int superJump = 0;
    private float superJumpMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        startPosition = rigidBody.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpKeyWasPressed = true;

        horizontalInput = Input.GetAxis("Horizontal") * 1.8f;

        if (rigidBody.position.y < -20)
            rigidBody.position = startPosition;
    }


    // updates with an alpha of the phisics instead of pear frame
    void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(horizontalInput, rigidBody.velocity.y, rigidBody.velocity.z);

        if (Physics.OverlapSphere(groundChecktransform.position, 0.1f, playerMask).Length == 0){
            if (superJump > 0)
                superJump--;
            else
                return;
        }

        if (jumpKeyWasPressed)
        {
            if (superJump > 0)
            {
                superJumpMultiplier = 1.2f;
                superJump--;
            }


            rigidBody.AddForce(Vector3.up * 7 * superJumpMultiplier, ForceMode.VelocityChange);
            superJumpMultiplier = 1;

            jumpKeyWasPressed = false;

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
