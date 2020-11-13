using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ray를 이용해서 부딪힌 곳에 총알자국 공장에서 총알자국을 만들고, 부딪힌 곳에 표시하고 싶다.
// - 총알자국
// - 총알자국 파티클시스템

public class Gun : MonoBehaviour
{
    enum GunType
    {
        Rifle,
        Snifer
    }


    public GameObject bulletImpact;
    public GameObject bulletPoint;
    private ParticleSystem[] bulletImpactParticleSystem;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        zoomFOV = 10;
        normalFOV = 50;
        fovTarget = 60;
        zoomSpeed = 9f;
        lr = GetComponent<LineRenderer>();
        // 태어날 때 파티클시스템을 가져오고 싶다.
        bulletImpactParticleSystem = bulletImpact.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
        Shot();


        // 1. 시선을 만들고
        // 2. 담을 곳의 정보를 담는 변수를 만든다.
        // 3. 바라보고 싶다.
        // 4. 시선이 무언가에 닿으면
        // 5. 총알자국을 만드고
        // 6. 그 곳에 총알자국 표시
        // 7. 3초 뒤 총알자국 삭제.
        if (Input.GetButtonDown("Fire1"))
        {
            print(bulletPoint.transform.position);
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {

                // bulletImpact.transform.position = Vector3.Reflect(Camera.main.transform.position, hitInfo.normal);
                
                bulletImpact.transform.position = hitInfo.point;
                // bulletImpact.transform.forward = Vector3.Reflect(transform.position, hitInfo.normal);
                bulletImpact.transform.forward = hitInfo.normal;

                // hitInfo.normal 이 법선벡터, Vector3.Reflect(originalObject.position, hitInfo.normal) 이 반사각!
                // 반사각은?
                // reflectedObject.position = Vector3.Reflect(originalObject.position, Vector3.right);
                // public Transform target;

                // // prints "close" if the z-axis of this transform looks
                // // almost towards the target

                // void Update()
                //{
                //    Vector3 targetDir = target.position - transform.position;
                //    float angle = Vector3.Angle(targetDir, transform.forward);
                //    if (angle < 5.0f)
                //        print("close");
                //}

                for (int i = 0; i < bulletImpactParticleSystem.Length; i++)
                {
                    // bulletImpact.transform.LookAt(Camera.main.transform);
                    bulletImpactParticleSystem[i].Stop();
                    bulletImpactParticleSystem[i].Play();
                }

                if (hitInfo.transform.name.Contains("Enemy"))
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
                    enemy.DoDamage(1);
                    // enemy._state = Define.State.Stunned;
                    // Destroy(hitInfo.transform.gameObject);
                }
                StartCoroutine(DrawLine(hitInfo.transform.position));
                // 만약 부딛힌 게임오브젝트의 이름에 Enemy 가 포함되어 있다면,
                // 파괴하고 싶다.

                // Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.red, 1f);
                // Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 30f, Color.red, 1f);
                // print($"{hitInfo.point} / {hitInfo.transform.name}");
            }
        }

        // VR 에서는 레이를 발생하고 있는 중에 인터랙션이 발생하므로
        // 레이를 실행한 상태에서 입력을 받는다.
        // 직선상으로 선만 그리고 레이는 입력 받았을 때 표현하는 것이 부하가 좀 적지 않을까...라는 생각...
        // 충돌인정을 받으려면 콜라이더가 있어야 함.
    }

    IEnumerator DrawLine(Vector3 hitPoint)
    {
        lr.SetPosition(0, bulletPoint.transform.position);
        print(bulletPoint.transform.position);
        // lr.SetPosition(0, Camera.main.transform.position);
        lr.SetPosition(1, hitPoint);
        lr.enabled = true;
        yield return new WaitForSeconds(0.01f);
        lr.enabled = false;
    }

    void Shot()
    {

    }

    public float zoomFOV = 10;
    public float normalFOV = 50;
    public float zoomSpeed = 1;
    float fovTarget;

    void Zoom()
    {
        // float target = Camera.main.fieldOfView; 주소를 가져오는게 아니고 값을 가져오는거라서 안되는 것으로 판단됨.

        if (Input.GetMouseButtonDown(1))
        {
            fovTarget = zoomFOV;
        }
        if (Input.GetMouseButtonUp(1))
        {
            fovTarget = normalFOV;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovTarget, zoomSpeed * Time.deltaTime);
    }



}
