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

    int jumpCount = 0;
    int maxJumpCount = 2;

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
        // 만약, 땅에 서있다.
        // 그리고, 사용자가 점프 버튼을 누른다면,
        // cc 로 부위별 충돌여부를 알고 싶을 때 : cc.collisionFlags & CollisionFlags.CollideBelow
        // 조건은 왼쪽에서 부터 체크하므로 빠른 결정이 나는 조건을 제일 왼쪽으로! (뒷 부분 연산 안하도록)
        // if (cc.isGrounded && Input.GetButtonDown("Jump"))
        if (cc.isGrounded)
        {
            jumpCount = 0;
        }

        if ((jumpCount < maxJumpCount) && Input.GetButtonDown("Jump"))
        {
            velocityY = jumpPower;
            ++jumpCount;
            // print(jumpCount + "/" + maxJumpCount);
        }

        // 사용자의 입력을 받는다.
        // 앞뒤좌우로 방향을 만든다.
        // 그 방향으로 이동하고 싶다.


        // 카메라의 좌표에서 앞뒤좌우로 이동하고 싶다.

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 dir = new Vector3(x, 0f, z);
        // 카메라를 기준으로 이동하고 싶다.
        dir = Camera.main.transform.TransformDirection(dir); // dir 을 카메라를 기준으로 바꿔준다.

        dir.Normalize();
        dir *= speed;
        dir.y = velocityY;

        cc.Move(dir * Time.deltaTime);

        // print($"{velocityY} / {dir.y}");

        //transform.position += dir * Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * velocityY);
    }

}
