using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform player;
    float rotationOfX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hides cursor 
    }

    // Update is called once per frame
    void Update()
    {
        // Get input of mouse rotations
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationOfX -= mouseY;
        rotationOfX = Mathf.Clamp(rotationOfX, -90f, 90f); // Need to clamp so no over rotation on y axis

        transform.localRotation = Quaternion.Euler(rotationOfX, 0f, 0f); // Move view up and right
        player.Rotate(Vector3.up * mouseX);  // Move view left and right


    }
}
