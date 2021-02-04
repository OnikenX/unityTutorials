using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //a public reference to the player mesh
    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {
        //gives lock to the cursor in the for it to not 
        Cursor.lockState = CursorLockMode.Locked;
    }


    float mouseX = 0f;
    float mouseY = 0f;
    float mouseSensitivity = 100f;
    private float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        UpdateCameraMovement();
    }

    private void UpdateCameraMovement()
    {
        //getting input from the input system(legacy, TODO: see the new system)
        //multiplying by the mouse sensitivity
        //multiplying by the deltaTime so it is constant no matter the fps in the game
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //giving the rotation to the mouse
        //it is subtracted because, if we add it, it will be inverted
        xRotation -= mouseY;

        //clamping the vision(limiting) for that the vision doesn't goes upside down and stay always behind the player
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //to apply the rotation to the actual element
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //rotating the player
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
