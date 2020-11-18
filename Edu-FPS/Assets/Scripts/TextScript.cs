using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    Text dieText;
    string sDieText;
    List<string> text;

    // Start is called before the first frame update
    void Start()
    {
        dieText = GetComponent<Text>();
        sDieText = "You Die.";
        StopCoroutine(TextStripting());
        StartCoroutine(TextStripting());
    }

    IEnumerator TextStripting()
    {
        dieText.text = "";
        for (int i = 0; i < sDieText.Length; i++)
        {
            dieText.text += sDieText[i];
            yield return new WaitForSeconds(0.4f);
        }
    }

}
