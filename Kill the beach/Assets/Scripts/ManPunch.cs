﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManPunch : StateMachineBehaviour
{

    public CitizenMovement CitizenMovement; 
    float Distance;
    Transform PlayerPos;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //CitizenMovement = GameObject.FindObjectOfType<CitizenMovement>();
        //CitizenMovement = EnemyCitizen.GetComponent<CitizenMovement>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Distance = Vector2.Distance(animator.transform.position, PlayerPos.position);

        if(Distance > CitizenMovement.EnemySafeDistance + 0.1f )  
            animator.SetBool("Hit",false);
        //if(Distance < CitizenMovement.EnemyUnSafeDistance)
          //  animator.SetBool("Hit",false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
