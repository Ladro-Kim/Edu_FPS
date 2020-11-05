using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slerp : MonoBehaviour
{

    Vector3 destPos = new Vector3(10, 10, -18.65f);
    float t, s;
    public AnimationCurve ac;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t = 0.5f * Time.deltaTime;
        s = ac.Evaluate(Time.time);

        transform.position = Vector3.Slerp(transform.position, destPos, t);
        transform.rotation = Quaternion.Lerp(Quaternion.EulerAngles(0, 0, 0), Quaternion.LookRotation(destPos), s);
        
    }
}
