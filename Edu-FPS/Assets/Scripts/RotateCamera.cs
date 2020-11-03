﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스의 이동값을 받아서 카메라를 회전시키고 싶다.


public class RotateCamera : MonoBehaviour
{
    float rX, rY;
    public float rotSpeed = 200f; // 증폭값으로 표현하는 경우가 있음. 마우스 민감도값 변경이 가능해야 하기 때문에 public 으로 설정.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");

        // float scroll = Input.GetAxis("Mouse ScrollWheel");
        // print(scroll);

        print($"{rX} / {rY}");

        rX += xInput * rotSpeed * Time.deltaTime;
        rY += yInput * rotSpeed * Time.deltaTime;

        rY = Mathf.Clamp(rY, -80, 80);

        transform.eulerAngles = new Vector3(-rY, rX, 0);

    }
}
