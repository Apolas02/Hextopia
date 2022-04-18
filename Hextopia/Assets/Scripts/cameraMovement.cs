using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float RotationSpeed = 1;
    public float mouseX, mouseY, mouseUpdateX;
    
    private Vector3 Target;
    public Transform targetPos, Player;
    public PlayerMovement mouseUpdate;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        
        Target = targetPos.position;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            updateMouse();
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CamControl();
        }

        else
        {
            //updateMouse();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 50);

        transform.LookAt(Target);

        targetPos.rotation = Quaternion.Euler(mouseY, mouseX, 0);

    }

    public void updateMouse()
    {
        mouseX = mouseUpdate.turnAmount;
    }

}
