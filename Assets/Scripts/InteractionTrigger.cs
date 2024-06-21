using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject text;

    [SerializeField] private UnityEvent startInteraction;
    [SerializeField] private UnityEvent stopInteraction;

    private bool isInteractiveScreen;

    private bool isPlayerInTrigger;
    // Start is called before the first frame update
    void Start()
    {
        //text = FindObjectOfType<FPSController>().gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetButtonUp("Use"))
            {
                if (isInteractiveScreen)
                {
                    isInteractiveScreen = false;
                    stopInteraction.Invoke();
                }
                else
                {
                    isInteractiveScreen = true;
                    startInteraction.Invoke();
                }
                SetCursorState();
            }
        }
    }

    private void SetCursorState()
    {
        if (Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
    }
    private void OnTriggerEnter(Collider other)
    {
        text.SetActive(true);
        isPlayerInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
        isPlayerInTrigger = false;
    }
}
