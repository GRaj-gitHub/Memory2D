using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject turnText;
    public GameObject minText;
    public GameObject secText;

    private void Start()
    {
        turnText.GetComponent<Text>().text = PlayerPrefs.GetInt("Score") + "";
        minText.GetComponent<Text>().text = PlayerPrefs.GetString("Min");
        secText.GetComponent<Text>().text = PlayerPrefs.GetString("Sec");
    }
    
}
