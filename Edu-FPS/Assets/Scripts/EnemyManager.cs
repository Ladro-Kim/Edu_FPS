using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// 일정시간마다 적 공장에서 적을 만들고 내 위치에 가져다 놓고 싶다.
// - 생성시간
// - 현재시간
// - 에너미 프리펩


public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPref;

    // Random Time.
    // public float spawnMin = 1f;
    // public float spawnMax = 3f;
    public float spawnTime = 2f;
    float currentTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        enemyPref = (GameObject) Resources.Load("Prefebs/Enemy");
        if (enemyPref == null)
        {
            print("Prefebs/Enemy is null");
        }
        int ret;
        Plus(10, 20, out ret);

    }

    void Plus(int a, int b, out int Result)
    {
        Result = a + b;
    }

    void Minus(int a, int b, ref int result)
    {

    }



    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > spawnTime && enemyPref != null)
        {
            GameObject tempEnemy = Instantiate(enemyPref);
            tempEnemy.transform.position = GetSpawnPosition();
            tempEnemy.transform.forward = transform.forward;
            currentTime = 0;
        }
    }

    private Vector3 GetSpawnPosition()
    {

        float x = Random.Range(0, transform.localScale.x);
        float z = Random.Range(0, transform.localScale.z);

        Vector3 lt = transform.position;
        lt.x -= transform.localScale.x / 2;
        lt.z -= transform.localScale.z / 2;

        Vector3 origin = lt + new Vector3(x, 0, z);

        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.DrawRay(origin, Vector3.down * 100f, Color.red, 1f);
        }
        else
        {
            print("hit null");
        }
        // gameObject.transform.scale

        // 범위를 구하고
        // 랜덤값을 정하고
        // 위치를 만든다.
        // 그 위치에서 아래 방향으로  Ray
        // RaycastHit.transform.position 값 반환.

        return hit.point;

    }
}
