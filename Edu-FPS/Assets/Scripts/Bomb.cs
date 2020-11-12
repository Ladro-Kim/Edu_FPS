using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 계속 자신의 앞 방향으로 물리엔진을 이용한 등속도 운동을 하고싶다.
    // - 강체
    // - 속력
    Rigidbody myRigid;
    public float speed = 1f;
    public float power = 10f;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();

        myRigid.velocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = myRigid.velocity.normalized;
        // myRigid.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
    }
}
