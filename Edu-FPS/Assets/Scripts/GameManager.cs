using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UI_gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        UI_gameOver.SetActive(false);
    }



    public void OnClickRestart()
    {
        Time.timeScale = 1; // 시간정지 원복.
        
        // 현재 씬 재시작
        // print("OnClickRestart()");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Static 값은 초기화 되지 않음...임의로 초기화를 직접 해줘야 함...
        // SceneManager.LoadSceneAsync -> 씬을 로딩하면서 다른화면(로딩화면)보여줄 때 사용. 유니티는 싱글스레드....
    }

    public void OnClickQuit()
    {

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_ANDROID
            Application.Quit();
    #elif UNITY_IOS
            Application.Quit();
    #else
            Application.Quit();
    # endif

        // print("OnClickQuit()");

        // 유니티상에서 종료할 때 사용. 
        // UnityEditor.EditorApplication.isPlaying = false; (유니티상에서 툴을 만들어 사용할 때...)

    }
}
