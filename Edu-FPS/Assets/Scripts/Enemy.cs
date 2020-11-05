using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 길찾기 기능(Navigation Mesh Agent)을 이용해서 목적지를 향해 이동하고 싶다.
// - 이동속력
// - 길찾기 기능(NavMeshAgent) <- UnityEngine.AI 기능이므로 using 에 추가해야 함.

public class Enemy : MonoBehaviour
{
    float speed = 3.5f;
    NavMeshAgent navMeshAgent;
    
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        // 길찾기 기능에게 목적지를 알려주고 싶다.
        // navMeshAgent 에게 목적지를 target 의 위치로 하라고 알려주고 싶다.

    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position); // navMeshAgent.destination = target.position;
    }
}
