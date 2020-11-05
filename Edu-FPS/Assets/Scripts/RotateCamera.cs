using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스의 이동값을 받아서 카메라를 회전시키고 싶다.


public class RotateCamera : MonoBehaviour
{
    float rX, rY;
    public float rotSpeed = 200f; // 증폭값으로 표현하는 경우가 있음. 마우스 민감도값 변경이 가능해야 하기 때문에 public 으로 설정. 경험에 의한 200f.

    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 rX 와 rY 의 현재 회전정보를 대입하고 싶다.
        rX = transform.eulerAngles.y; // 각도를 받아옴. -> 마우스의 값을 받아오는게 아니라서 다시 거꾸로 설정.
        rY = transform.eulerAngles.z;
        
        //Vector3 curRot = transform.eulerAngles;
        //rX = curRot.x;
        //rY = curRot.y;
        print($"{rX} / {rY}");
        
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Mouse X"); // 마우스의 위치값을 받아옴
        float yInput = Input.GetAxis("Mouse Y");

        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // print(scroll);

        // print($"{rX} / {rY}");

        rX += xInput * rotSpeed * Time.deltaTime;
        rY += yInput * rotSpeed * Time.deltaTime;

        rY = Mathf.Clamp(rY, -80, 80);

        transform.eulerAngles = new Vector3(-rY, rX, 0);

    }
}
