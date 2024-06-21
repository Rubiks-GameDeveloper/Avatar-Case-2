using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3;
    [SerializeField] private float runningSpeed = 5;
    [SerializeField] private GameObject groundChecker;
    private CharacterController _characterController;
    private Vector3 _velocity;
    private GameObject _ground;
    //public Animator Animator { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 90;
        _characterController = GetComponent<CharacterController>();
        
        //Animator = GetComponentInChildren<Animator>();
        //Animator.SetFloat("Transitor", 1);
    }
    
    private void FixedUpdate()
    {
        MovementApplier();
        GravityApplier();
    }

    private void MovementApplier()
    {
        var transformData = transform;
        var moveTrajectory = transformData.right * Input.GetAxis("Horizontal") +
                             transformData.forward * Input.GetAxis("Vertical");
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            //Animator.SetFloat("Transitor", 0, 0.2f, Time.fixedDeltaTime);
        }
        else if (Input.GetAxis("Vertical") > 0.55f)
        {
            //Animator.SetFloat("Transitor", 1, 0.15f, Time.fixedDeltaTime);
            _characterController.Move(moveTrajectory * Time.fixedDeltaTime * runningSpeed);
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //Animator.SetFloat("Transitor", 0.5f * Math.Abs(Input.GetAxis("Horizontal")), 0.15f, Time.fixedDeltaTime);
            _characterController.Move(moveTrajectory * Time.fixedDeltaTime * movementSpeed);
        }
    }

    private void GravityApplier()
    {
        if (!IsGrounded())
        {
            _velocity.y -= 9.81f / 2 * Mathf.Sqrt(Time.fixedDeltaTime);
        }
        else
        {
            _velocity.y = 0;
        }
        _characterController.Move(_velocity * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        var results = new Collider[20];
        Physics.OverlapSphereNonAlloc(groundChecker.transform.position, 0.3f, results);
        
        return results.Any(item => item != null && item.CompareTag("Ground"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundChecker.transform.position, 0.3f);
    }
}
