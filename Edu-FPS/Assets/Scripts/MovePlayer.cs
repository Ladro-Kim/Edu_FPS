using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

// y방향으로 중력을 구현하고 점프도 구현하고 싶다.
// - Gravity
// - JumpPower
// - Velocity Y

// 캐릭터 컨트롤러를 이용해서 충돌처리를 하고싶다.

[RequireComponent(typeof(CharacterController))]

public class MovePlayer : MonoBehaviour
{
    CharacterController cc;

    public float gravity = -9.81f;
    public float jumpPower = 5f;
    public float velocityY;

    // 사용자의 입력에 따라 앞뒤좌우로 방향을 만들고 그 방향으로 이동하고 싶다.
    // - speed(속력)                 * Velocity(속도), 크기와 방향을 가지는 값.
    // - P = P0 + VT
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>(); // 없으면 널값이 들어간다.
    }

    // Update is called once per frame
    void Update()
    {

        // y 방향에 1초동안 중력값을 누적하여 더하고 싶다.
        // 최종 이동할 y 방향에 y방향을 대입하고 싶다. (dir.y)
        velocityY += gravity * Time.deltaTime;

        // 만약, 사용자가 점프 버튼을 누르면, 점프를 뛰고싶다.
        // Y속도에 점프파워를 대입하고 싶다.
        if (Input.GetButtonDown("Jump"))
        {
            velocityY = jumpPower;
        }

        // 사용자의 입력을 받는다.
        // 앞뒤좌우로 방향을 만든다.
        // 그 방향으로 이동하고 싶다.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 dir = new Vector3(x, 0f, z);
        dir.Normalize();
        dir *= speed;
        dir.y = velocityY;

        cc.Move(dir * Time.deltaTime);

        print($"{velocityY} / {dir.y}");

        //transform.position += dir * Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * velocityY);
    }

}
