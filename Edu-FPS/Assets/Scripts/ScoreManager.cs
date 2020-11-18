using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 점수처리를 하고싶다.
// 처음에는 0 점으로 시작
// 적을 죽이면 1점 증가
// UI 에도 반영하고 싶다.
// - 점수
// - UI
// 레벨을 표현하고 싶다.
// 레벨은 1 부터 시작하고
// 레벨과 같은 수의 적을 죽이면 레벨업.
// - 레벨
// - 경험치
// - UI
// 경험치를 표시하고 싶다.
// - 현재 경험치, 최대 경험치
// - UI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public delegate void dInitValue();
    public dInitValue initValue;

    // public Action<int> initValue;

    int level = 0;
    public int LEVEL
    {
        get
        {
            return level;
        }
        set
        {

            level = value;
            text_level.text = $"Lv : {level}";
        }
    }
    int killCount;
    public int KILLCOUNT
    {
        get
        {
            return killCount;
        }
        set
        {
            killCount = value;
            slider_exp.value = killCount;
            text_percent.text = $"{((killCount / (float)LEVEL) * 100.0f).ToString("N3")}%";
        }
    }

    public Text text_score;
    public Text text_level;
    public Slider slider_exp;
    public Text text_percent;

    int score = 0;
    public int SCORE
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            text_score.text = $"Score : {score}";

            // 점수를 초기화 할때는 킬카운트가 증가하지 않게 하고싶다.
            // -> 초기화가 아닐 때만 킬 카운트가 증가되게 하고싶다.
            if (value != 0)
            {
                KILLCOUNT++;
                if (killCount == LEVEL)
                {
                    LEVEL++;

                    if (initValue != null)
                    {
                        initValue();
                    }

                    // levelUp.Invoke();
                    // EnemyManager.instance.createCount = 0;
                    //StopCoroutine(ieLEVEL_UP());
                    //StartCoroutine(ieLEVEL_UP());
                    Invoke("InitExpUI", 1f); // 일정시간 뒤에 함수를 호출해라. 자기자신을 호출하는, 재귀함수.
                    // 인보크를 쓸꺼면 인보크를, 코루틴을 쓸꺼면 코루틴을 사용할 것. 1가지 중심으로!
                }
            }

        }
    }

    private void Awake() // 생성은 Awake 에서 생성. Start에서 사용할 수도 있기 때문에...
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void InitExpUI()
    {
        KILLCOUNT = 0;
        slider_exp.maxValue = LEVEL;
    }


    //IEnumerator ieLEVEL_UP()
    //{
    //    yield return new WaitForSeconds(1f);
    //    KILLCOUNT = 0;
    //    slider_exp.maxValue = LEVEL;

    //}


    void Start()
    {
        SCORE = 0;
        LEVEL = 1;
        slider_exp.maxValue = LEVEL;
        KILLCOUNT = 0;
    }

    void Update()
    {
        
    }
}
