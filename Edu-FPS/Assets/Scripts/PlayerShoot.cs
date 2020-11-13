using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// G 버튼을 눌렀을 때 카메라가 바라보는 방향으로 폭탄 프리펩을 만들어서 카메라 위치에 가져다 놓고 싶다.
// 폭탄의 앞 방향을 카메라의 앞 방향으로
// 


public class PlayerShoot : MonoBehaviour
{
    GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        bomb = (GameObject) Resources.Load("Prefebs/Bomb");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject tempBomb = Instantiate(bomb);
            tempBomb.transform.position = Camera.main.transform.position;
            bomb.transform.forward = Camera.main.transform.forward;
        }
    }
}
