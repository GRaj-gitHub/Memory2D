using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    [SerializeField]
    private AudioSource match;

    public Sprite[] cardImage =  new Sprite[12];
   
    public Sprite card_bg; // the original backside of the card
   
    //matrix containing the cards;
    public GameObject[,] mtrx_cards = new GameObject[4, 3];

    //Matrix containing the images
    public Sprite[,] mtrx_image = new Sprite[4, 3];
   
    //Matrix to check if image loaded or not
    private bool[] set = { false, false, false, false, false, false, false, false, false, false, false, false }; //intially set to false;

    int click_limit = 0;   //more than two cards are open

    private GameObject[] nowOpen = new GameObject[2];

    public GameObject turns;
    public int turn_count; //no of turns to complete the game
    int setcount = 0; // to check all card have opened



    void Start()
    {
 
        for (int i =  0; i < 4; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                mtrx_cards[i, j] = GameObject.Find("card"+ i + "" + j + "");

                cardAssign(i, j);
               
               // Debug.Log(mtrx_image[i, j]);               
               
            }
        }

        
    }

    void cardAssign(int x, int y)
    {
        int random_selection = Random.Range(0, 12) % 12;
       
        if (set[random_selection] == false)
        {
            mtrx_image[x, y] = cardImage[random_selection];
            set[random_selection] = true;
        }
        else
        {
            cardAssign(x, y);
        }
               
    }


public void DisplayOnCLick()
    {
       
       // Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {

               
                if (EventSystem.current.currentSelectedGameObject.name == mtrx_cards[i, j].name)
                {
                     mtrx_cards[i, j].GetComponent<Image>().sprite = mtrx_image[i, j];
                    // Debug.Log(mtrx_image[i, j]);
                }
                

            }
        }

        if (click_limit <= 1)
        {
            StartCoroutine(checkCards(EventSystem.current.currentSelectedGameObject));
        }

    }

    IEnumerator checkCards(GameObject now_open)
    {
        nowOpen[click_limit] = now_open;
        if(click_limit == 1)
        {
            if (nowOpen[click_limit] != nowOpen[click_limit - 1] && nowOpen[click_limit].GetComponent<Image>().sprite == nowOpen[click_limit - 1].GetComponent<Image>().sprite)
            {
                yield return new WaitForSeconds(0.25f);
                match.Play();
                nowOpen[click_limit].SetActive(false);
                nowOpen[click_limit - 1].SetActive(false);
                set[click_limit] = false;
                set[click_limit - 1] = false;
                click_limit = 0;
            }
            else 
            {
                StartCoroutine(flip_cards());
                click_limit = 0;
            }
            turn_count++;
            if (turn_count < 10)
            {
                turns.GetComponent<Text>().text = "0" + turn_count + "";
            }
            else
                turns.GetComponent<Text>().text = turn_count + "";

        }
        else if (click_limit == 0)
        {
            ++click_limit;
        }
        
        for(int i = 0; i < 12; i++)
        {
            if(set[i] == false)
            {
                set[i] = true;
                setcount++;
                Debug.Log(setcount);
                if(setcount == 12)
                {
                    PlayerPrefs.SetInt("Score", turn_count);
                    SceneManager.LoadScene("Gameover", LoadSceneMode.Single);
                }
            }

        }
       
    }

    IEnumerator flip_cards()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i=0; i<4; i++)
        {
            for(int j=0; j<3; j++)
            {
                mtrx_cards[i, j].GetComponent<Image>().sprite = card_bg;
            }
        }

    }
}
