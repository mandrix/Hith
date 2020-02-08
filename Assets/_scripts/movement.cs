﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Variables")]
    public float movementSpeed = 10.0f;
    [Range(0,4)]
    public float runSpeedMultiplier = 1.5f;
    public float jumpForce = 10.0f;
    public float gravityScale = 2.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    [Space]
    [Header("Animación")]
    private Animator anim;
    
    [Space]
    [Header("Camara")]
    public Transform pivot;
    public float rotateSpeed;

    [Space]
    [Header("Testing para animaciones")]
    public float vAxis = 0.0f;
    public float hAxis = 0.0f;

    [Space]
    [Header("Air tank variables")]
    [SerializeField]
    [Range(0, 15)]
    [Tooltip("Cuanto oxígeno se usa al utilizar el botón de recoger")]
    private int airTankUsage = 7;
    [SerializeField]
    [Range(0, 5)]
    [Tooltip("Cuanto oxígeno se usa al utilizar el botón de recoger")]
    private float airTankUsageRun = 3;

    private bool actionFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if (vAxis!=0 || hAxis!=0) {
            cancelAnimations();
            actionFlag = true;
        }
        float coef = GetComponent<airTankLevel>().coef_copy;
        bool isRunning = false;

        if (Input.GetButton("Run") && characterController.isGrounded)
        {
            isRunning = true;
            GetComponent<airTankLevel>().coef = coef * airTankUsageRun;
        }
        else
        {
            GetComponent<airTankLevel>().coef = coef;
        }

        moveDirection = (transform.forward * vAxis) + (transform.right * hAxis);
        moveDirection = moveDirection.normalized * (isRunning ? movementSpeed * runSpeedMultiplier : movementSpeed);
        moveDirection.y = yStore;

        if (characterController.isGrounded && actionFlag)
        { 
            moveDirection.y = 0.0f;
            cancelAnimations();
            bool newActionFlag = true;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                newActionFlag = false;
            }

            else if (Input.GetButtonDown("Fire1") )
            {
                newActionFlag = false;
                anim.SetBool("gathering", true);
                GetComponent<airTankLevel>().reduceAir(airTankUsage);
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                anim.SetBool("dance", true);
                newActionFlag = false;
            }
            actionFlag = newActionFlag;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        characterController.Move(moveDirection * Time.deltaTime);

        // MOVE PLAYER IN DIFFERENT DIRECTIONS BASED ON CAMERA
        if (hAxis != 0 || vAxis != 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, pivot.rotation.eulerAngles.y, 0.0f);
        }

        anim.SetBool("isGrounded", characterController.isGrounded);
        anim.SetFloat("vSpeed", isRunning ? vAxis * 2 : vAxis);
    }

    private void cancelAnimations() {
        anim.SetBool("gathering", false);
        anim.SetBool("dance", false);
    }
}
