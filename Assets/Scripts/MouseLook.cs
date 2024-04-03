using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;
    float xRotation = 0f;
    [SerializeField]
    private Transform flashLightBody;

    [SerializeField]
    private bool controlsActive = true;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 65;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 18;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            flashLightBody.localRotation = transform.localRotation;
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void ActivateControls(bool value)
    {
        //Reset camera rotations.
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        //Reset flashlight rotation.
        flashLightBody.localRotation = Quaternion.Euler(0, 0f, 0f);
        controlsActive = value;
    }
}
