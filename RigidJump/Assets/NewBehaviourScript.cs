using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody myRigid;
    float jumpPower = 10;
    float speed = 3f;
    bool isGrounded = true;
    int jumpCount = 0;
    int maxJumpCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z);
        // dir = Camera.main.transform.TransformPoint(dir);
        dir.y = 0;
        dir.Normalize();

        myRigid.MovePosition(transform.position + dir * speed * Time.deltaTime);

        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            // Physics.gravity = new Vector3(0, -9.81f * 5, 0);
            myRigid.velocity = Vector3.zero;
            jumpCount++;
            isGrounded = false;
            myRigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Floor"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
