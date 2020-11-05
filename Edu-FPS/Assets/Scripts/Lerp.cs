using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    Vector3 destPos = new Vector3(10, 1, -14.64f);
    float t; // 배속이라는 개념으로.
    public AnimationCurve ac;
    public Transform a;
    public Transform b;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // t = ac.Evaluate(Time.time * 0.2f);
        // transform.position = Vector3.Slerp(a.position, b.position, t);
        // transform.
        // Vector3 dir = b.position - a.position;
        
        float distance = Vector3.Distance(a.position, b.position);
        if (distance < 0.1f)
        {
            transform.position = b.position;
        }
    }

}
