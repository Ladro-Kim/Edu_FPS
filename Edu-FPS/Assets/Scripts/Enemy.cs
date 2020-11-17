using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    // 만약 hp 가 0이 되면 넘어지고 파괴하게 하고싶다.
    public float curHp;
    public float maxHp = 3;
    public Slider sliderHp;

    public float HP
    {
        get
        {
            return curHp;
        }
        set
        {
            curHp = value;
            sliderHp.value = curHp;
            
        }
    }


    public Define.State _state;

    float speed = 3.5f;
    NavMeshAgent navMeshAgent;

    PlayerHp target;

    GameObject barbarian;

    public Animator animator;

    public bool isStop;

    // Start is called before the first frame update
    void Start()
    {
        // maxValue 만들고 시작하기.
        sliderHp.maxValue = maxHp;
        HP = maxHp;

        barbarian = transform.Find("Barbarian").gameObject;
        // animator = barbarian.GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        // agent.Warp(transform.position); 간헐적으로 안되는 경우가 많이 있어서 이렇게 하는 경우가 많음.
        // Warp : 현재위치와 가장 가까운 navMesh 위치로 올라감.
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
        // sliderHp.transform.LookAt(target);

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
            case Define.State.Stunned:
                break;
            default:
                break;
        }
        // isStop = navMeshAgent.isStopped;

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
        target = GameObject.FindObjectOfType<PlayerHp>();
        if (target != null)
        {
            navMeshAgent.SetDestination(target.transform.position); // navMeshAgent.destination = target.position;
            SetState(Define.State.Walk);
            //_state = Define.State.Walk;
            //navMeshAgent.isStopped = false;
            //animator.SetTrigger("Walk");
            // animator.CrossFade("Walk", 0.5f, 0);
        }
    }

    private void UpdateWalk()
    {
        navMeshAgent.SetDestination(target.transform.position); // navMeshAgent.destination = target.position;
        if (Vector3.Distance(transform.position, target.transform.position) <= navMeshAgent.stoppingDistance)
        {
            SetState(Define.State.Attack);
            //_state = Define.State.Attack;
            //navMeshAgent.isStopped = true;
            //animator.SetTrigger("Attack");
            // animator.CrossFade("Attack", 0.5f, 0);
        }

        // 1번만 해야하는 일, 계속 해야하는 일을 잘 구현할 것. (될 수 있으면 조건문을 사용해서...

    }

    private void UpdateAttack()
    {
        //if (Vector3.Distance(transform.position, target.transform.position) >= navMeshAgent.stoppingDistance)
        //{
        //    _state = Define.State.Walk;
        //    navMeshAgent.isStopped = false;
        //    animator.SetTrigger("Walk");
        //    // animator.CrossFade("Walk", 0.5f, 0);
        //}
    }

    internal void DoDamage(int damage = 1) // 매개변수 입력 안하는 경우, Default 값으로 1 이 들어감.
    {
        // 체력을 1씩 감소시키고 싶다.
        // 체력이 0이 되면, 넘어지는 상태로 전이.
        // 체력이 0이면 즉시 함수를 종료.
        if (HP <= 0)
        {
            return;
        }
        
        HP -= damage;

        if (HP <= 0)
        {
            SetState(Define.State.Stunned);
        }
        //// 넘어지는 상태로 전이하고 싶다.
        //_state = Define.State.Stunned;
        //// 이동도 멈추고 싶다.
        //navMeshAgent.isStopped = true;
        //// 애니메이션도 넘어지는 애니메이션을 실행하고 싶다.
        //animator.SetTrigger("Stunned");
    }

    void SetState (Define.State next)
    {
        // 상태를 다음상태로 변경하고,
        _state = next;

        // 걸을 수 있는지 처리하고
        if (next == Define.State.Walk)
        {
            navMeshAgent.isStopped = false;
        }
        else
        {
            navMeshAgent.isStopped = true;
        }

        // 애니메이션 변경
        animator.SetTrigger(next.ToString());

    }






    // 공격 Hit되는 순간의 이벤트를 받고싶다.
    public void OnHit()
    {
        // 타겟에게 데미지를 입힌다.
        if (Vector3.Distance(transform.position, target.transform.position) <= navMeshAgent.stoppingDistance)
        {
            target.OnDamaged();
            print("OnHit");
        }


    }
    // 공격 애니메이션이 끝나는 순간의 이벤트를 받고싶다.
    public void OnFinishAttack()
    {
        // Destroy(gameObject, 1.5f);
    }



    // 공격 애니메이션이 끝나는 순간의 이벤트를 받고싶다.
    //public void OnFinishAttackBackup()
    //{
    //    print("OnFinishAttack");
    //    // 타겟이 유효 공격범위 내에 있지 않다면 Walk 상태로 전이.
    //    // 살아있는 타겟이 유효 공격범위 내에 있으면 공격상태로.
    //    //
    //    if (Vector3.Distance(transform.position, target.transform.position) >= navMeshAgent.stoppingDistance)
    //    {
    //        _state = Define.State.Walk;
    //        navMeshAgent.isStopped = false;
    //        // animator.CrossFade("Walk", 0.5f, 0);
    //        animator.SetTrigger("Idle");
    //    }
    //}

    void OnFinishStunned()
    {   
        //if (Vector3.Distance(transform.position, target.transform.position) <= navMeshAgent.stoppingDistance) {
        //    _state = Define.State.Walk;
        //}
        //else
        //{
        //    _state = Define.State.Idle;
        //}

        ScoreManager.instance.SCORE++;

        Destroy(gameObject);
        // 만약 목적지와의 거리가 공격가능한 거리라면 공격,
        // 그렇지 않다면 이동상태로 전이
    }

    private void OnApplicationQuit()
    {
        // 종료 직전에 호출되는 내용.
    }




    // 재업

}
