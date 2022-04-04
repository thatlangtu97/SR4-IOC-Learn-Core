﻿using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUIController : MonoBehaviour
{
    public static GameUIController instance;
    public Joystick Joystick;
    public CameraFollow cameraFollow;

    public StateMachineController stateMachine;

    public LayerMask maskToolTest;

    public PlayerInput playerInput;

    public Transform HpBarLeft;

    public Transform HpBarRight;
    
    private bool useRayCastTest;

    public GameEntity EntityController;
    public HPBarUI SpawnHPBar(HPBarUI hpBarUi,bool left)
    {
        if (left)
        {
            for(int i = HpBarLeft.childCount-1;i>=0;i--)
            {
                Destroy(HpBarLeft.GetChild(i).gameObject);
            }
            return Instantiate(hpBarUi, HpBarLeft);
        }
        else
        {
            for(int i = HpBarRight.childCount-1;i>=0;i--)
            {
                Destroy(HpBarRight.GetChild(i).gameObject);
            }
            return Instantiate(hpBarUi, HpBarRight);
        }
    }
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Button("MODIFY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void MODIFY()
    {
        if (!stateMachine) return;
        Joystick.componentManager = stateMachine.componentManager;
        if(cameraFollow)
            cameraFollow.player = stateMachine.gameObject;
    }
    [Button("RAYCAST TEST", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void RAYCASTTEST()
    {
        useRayCastTest = true;
    }
    
    private Gamepad gamePad;
    public Vector2 VectorMove;
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.D))
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            stateMachine.ChangeState(NameState.ReviveState, true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill1();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Skill2();
        }
        
        if (gamePad!=null)
        {

            if(gamePad.squareButton.wasPressedThisFrame || gamePad.buttonWest.wasPressedThisFrame)
                Attack();
            if(gamePad.circleButton.wasPressedThisFrame || gamePad.buttonEast.wasPressedThisFrame )
                Dash();
            if( gamePad.triangleButton.wasPressedThisFrame|| gamePad.buttonNorth.wasPressedThisFrame || gamePad.buttonSouth.wasPressedThisFrame || gamePad.crossButton.wasPressedThisFrame)
                Jump();
            if(gamePad.leftShoulder.wasPressedThisFrame || gamePad.leftTrigger.wasPressedThisFrame)
                Skill1();
            if(gamePad.rightShoulder.wasPressedThisFrame || gamePad.rightTrigger.wasPressedThisFrame)
                Skill2();
            Joystick.MoveGamePad(gamePad);
        }
        else
        {
            gamePad = Gamepad.current;
        }

        if(Input.GetMouseButtonDown(0) && useRayCastTest )
            RayCastChangeObject();
    }

    public void Dash(InputValue value)
    {
        
    }
    private void Start()
    {
        MODIFY();
        
    }
    public void Jump()
    {
        stateMachine.OnInputJump();
    }
    public void Dash()
    {
        stateMachine.OnInputDash();
    }
    public void Attack()
    {
        stateMachine.OnInputAttack();
    }
    public void Skill1()
    {
        stateMachine.OnInputSkill(0);
    }
    public void Skill2()
    {
        stateMachine.OnInputSkill(1);
    }
    public void Skill(int idSkill)
    {
        stateMachine.OnInputSkill(idSkill);
    }

    public void RayCastChangeObject()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position ,(Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized , 100f,maskToolTest);
        if(hit.collider != null)
        {
            Debug.Log ("Target Position: " + hit.collider.gameObject);
            if (hit.collider.gameObject.GetComponent<StateMachineController>() != null)
            {
                stateMachine = hit.collider.gameObject.GetComponent<StateMachineController>();
                MODIFY();
            }
        }
        Debug.DrawRay(Camera.main.transform.position , (Camera.main.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition) ).normalized  *100f,Color.blue);
    }



}
