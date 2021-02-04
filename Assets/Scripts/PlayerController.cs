
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDirection = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    bool jumping = false;

    [SerializeField] [Range(0.0f, 1000.0f)] float jumpMultiplier = 1;

    [SerializeField] private float superJumpCooldown = 200;

    float datejumped = 0;
    float jump = 0.0f;

    public int superJumpsLeft = 0;
    public int SuperJumpsLeft
    {
        get
        {
            return superJumpsLeft;
        }
        private set
        {
            superJumpsLeft = value;
            if (SuperJumpsLeftTextUI != null)
                SuperJumpsLeftTextUI.text = "Super Jumps Left : " + superJumpsLeft;

        }
    }

    [SerializeField] Text SuperJumpsLeftTextUI = null;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        SuperJumpsLeft = 0;
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {

        //gets input from the mouse
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //smooths the movement of the camera
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        //apply mouse sensitivity
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        //clamps the camera so it does not goes upside down
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //applyes the rotation for up and down to the camera
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        //applyes the rotation for right and left to the controller
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }


    void UpdateMovement()
    {
        //gets the values for input
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Jump input
        jump = Input.GetAxis("Jump");
        if (jump == 0)
            if (datejumped + superJumpCooldown > new System.DateTimeOffset().ToUnixTimeMilliseconds())
                jumping = false;

        //normalizes them so the diagonals instead of having ~1.41, every directions goes to 1 max
        targetDir.Normalize();

        //smooths out the movements so it does not do fast movements
        currentDirection = Vector2.SmoothDamp(currentDirection, targetDir, ref currentDirVelocity, moveSmoothTime);

        //stops the player if they are in ground
        if (controller.isGrounded)
        {
            velocityY = 0.0f;
            if (jump > 0)
                DoJump();
        }
        else
        {
            if (jump > 0 && SuperJumpsLeft > 0 && !jumping)
            {
                velocityY = 0.0f;
                SuperJumpsLeft--;
                DoJump();
            }
            else
            {
                // apply gravity to the player
                velocityY -= gravity * Time.deltaTime;
            }
        }

        // Debug.Log("Jump: " + jump + "; velocityY: " + velocityY + "; conta: " + (gravity * Time.deltaTime) + "; detatime: " + Time.deltaTime + ";gravity: " + gravity);

        Vector3 velocity =
        //walking movement
        (transform.forward * currentDirection.y + transform.right * currentDirection.x) * walkSpeed
        //falling/jumping velocity
         + Vector3.up * velocityY;

        //apply movement to the player
        controller.Move(velocity * Time.deltaTime);
    }

    void DoJump()
    {
        //math for jumps
        velocityY += jump * jumpMultiplier * Time.deltaTime;
        jumping = true;
        datejumped = new System.DateTimeOffset().ToUnixTimeMilliseconds();

        //TODO: animation trigger
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            ++SuperJumpsLeft;


        }
    }
}
