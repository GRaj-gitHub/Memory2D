using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
   
    public GameObject secObject;
   
    public GameObject minObject;

    private float second = 0.0f;
    private int minute = 0;
    // Update is called once per frame
    void Update()
    {
        second += Time.deltaTime;


        if (second < 10)
        {
            secObject.GetComponent<Text>().text = "0" + second + "";
        }
        else
        { 
            secObject.GetComponent<Text>().text =  second + "";
        }
        if(second > 60)
        {
            minute++;
            if(minute > 9)
            {
                minObject.GetComponent<Text>().text = minute + "" + ":";

            }
            else
            {
                minObject.GetComponent<Text>().text = "0" + minute + "" + ":";
            }
           
            second = 0.0f;
        }

        PlayerPrefs.SetString("Sec", secObject.GetComponent<Text>().text);
        PlayerPrefs.SetString("Min", minObject.GetComponent<Text>().text);
        
    }
}
