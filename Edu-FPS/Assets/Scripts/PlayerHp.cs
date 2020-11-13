using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 체력을 관리하고 싶다.
// 플레이어가 데미지를 입는 처리를 하고싶다.(번쩍)

public class PlayerHp : MonoBehaviour
{
    public GameObject hitUI;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hitUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamaged()
    {
        StopCoroutine(iePunch());
        StartCoroutine(iePunch());
    }

    IEnumerator iePunch()
    {
        hitUI.SetActive(true);
        yield return new WaitForSeconds(0.1f); // 잠깐 나가서 다른일 실행하고 조건이 만족되면 돌아와서 밑에 내용 실행.
        hitUI.SetActive(false);
    }


}
