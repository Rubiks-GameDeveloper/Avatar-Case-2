using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float horizontalMouseSens;
    [SerializeField] private float verticalMouseSens;
    private float _xRotation;
    private Transform _fpsParent;
    private bool _isPointExit;
    private Camera _cam;

    private Vector2 _previousMousePos;
    
    private void Start()
    {
        _cam = Camera.main;
        _fpsParent = transform.parent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        else if (Input.GetButton("Cancel") && Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
        CameraRotator();
    }

    private void CameraRotator()
    {
        //if (_isPointExit) return;
        var mouseX = Input.GetAxis("Mouse X") * horizontalMouseSens/* * Time.deltaTime*/;
        var mouseY = Input.GetAxis("Mouse Y") * verticalMouseSens /* * Time.deltaTime*/;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        Debug.Assert(Camera.main != null, "Camera.main != null");
        _cam.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        _fpsParent.Rotate(Vector3.up * mouseX);
    }
}
