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

    // Update is called once per frame
/// <summary>
/// 업데이트   <- 함수에 대한 설명
/// </summary>
    void Update()
    {
        transform.forward = myRigid.velocity.normalized;
        // myRigid.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
    }

    // - 폭발에 대한 반경
    public float explosionRadius = 3f;
    private void OnCollisionEnter(Collision other)
    {
        // 폭탄이 어딘가에 부딛힌 순간
        // 범위 내에 있는 적들을 죽이고
        int layer = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] colliderList = Physics.OverlapSphere(transform.position, explosionRadius, layer);
        
        // 에너미 컴포넌트에게 3의 데미지를 입히고 싶다.
        for (int i = 0; i < colliderList.Length; i++)
        {
            if (colliderList[i].gameObject.name.Contains("Enemy"))
            {
                Enemy enemy = colliderList[i].GetComponent<Enemy>();
                enemy.DoDamage(10);
            }
        }
        // 나 죽는다.
        Destroy(gameObject);
    }

    //IEnumerator bombExp(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    // 폭탄이 어딘가에 부딛힌 순간
    //    // 범위 내에 있는 적들을 죽이고
    //    Collider[] colliderList = Physics.OverlapSphere(transform.position, explosionRadius);
    //    for (int i = 0; i < colliderList.Length; i++)
    //    {
    //        Destroy(colliderList[i]);
    //    }

    //    // 나 죽는다.
    //    Destroy(gameObject);
    //}

}
