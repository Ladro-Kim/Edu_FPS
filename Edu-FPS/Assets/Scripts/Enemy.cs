using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 길찾기 기능(Navigation Mesh Agent)을 이용해서 목적지를 향해 이동하고 싶다.
// - 이동속력
// - 길찾기 기능(NavMeshAgent) <- UnityEngine.AI 기능이므로 using 에 추가해야 함.

// 애니메이션을 변경하고 싶다. -> Animator
// - Animator

// FSM 을 이용해서 상태를 제어하고 싶다.
// 대기, 이동, 공격

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour
{
    public Define.State _state;

    float speed = 3.5f;
    NavMeshAgent navMeshAgent;

    Transform target;

    GameObject barbarian;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        barbarian = transform.Find("Barbarian").gameObject;
        animator = barbarian.GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        // 상태를 바꿀 때 뭔가를 같이 수정해준다.
        _state = Define.State.Idle; 
        navMeshAgent.isStopped = true;

        // 길찾기 기능에게 목적지를 알려주고 싶다.
        // navMeshAgent 에게 목적지를 target 의 위치로 하라고 알려주고 싶다.

    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Walk:
                UpdateWalk();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
            default:
                break;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    animator.CrossFade("Idle", 0.5f, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    animator.CrossFade("Walk", 0.5f, 0);
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    animator.CrossFade("Attack", 0.5f, 0);
        //}



        // dir.magnitude

    }




    private void UpdateIdle()
    {
        // 타깃을 검색해서 있으면 이동상태로 전이하고 싶다.
        
        target = GameObject.Find("Player").transform;
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position); // navMeshAgent.destination = target.position;
            _state = Define.State.Walk;
            navMeshAgent.isStopped = false;
            animator.CrossFade("Walk", 0.5f, 0);
        }
    }

    private void UpdateWalk()
    {
        navMeshAgent.SetDestination(target.position); // navMeshAgent.destination = target.position;
        if (Vector3.Distance(transform.position, target.transform.position) <= navMeshAgent.stoppingDistance)
        {
            _state = Define.State.Attack;
            navMeshAgent.isStopped = true;
            animator.CrossFade("Attack", 0.5f, 0);
        }

        // 1번만 해야하는 일, 계속 해야하는 일을 잘 구현할 것. (될 수 있으면 조건문을 사용해서...

    }

    private void UpdateAttack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >= navMeshAgent.stoppingDistance)
        {
            _state = Define.State.Walk;
            navMeshAgent.isStopped = false;
            animator.CrossFade("Walk", 0.5f, 0);
        }
    }






}
