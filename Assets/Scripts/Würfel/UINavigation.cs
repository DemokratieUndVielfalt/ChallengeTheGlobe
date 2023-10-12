using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigation : MonoBehaviour
{
    PhoneChat phoneChat;
    [SerializeField] List<Country> countries = new List<Country>();
    [SerializeField] List<Texture> images = new List<Texture>();
    [SerializeField] List<string> moreInfo = new List<string>();
    int intA = 1;

    [SerializeField] string beginAnswer;
    [SerializeField] string endAnswer;
    [SerializeField] GameObject restartButton;

    string currRespose;
    // Start is called before the first frame update
    private void Start()
    {
        phoneChat = GetComponent<PhoneChat>();
        //CompareSolutions();
        GameObject[] countriesArray = GameObject.FindGameObjectsWithTag("Continent");

        foreach (GameObject country in countriesArray)
        {
            countries.Add(country.GetComponent<Country>());
        }
    }
    void CompareSolutions()
    {
        foreach (Country country in countries)
        {
            if (country.rightNR == intA)
            {
                if (country.currNR == country.rightNR)
                {
                    currRespose = country.rightAnswer + "Du hast so viel getippt " + country.currNR + " und das war auch richtig" + country.name;
                }
                else
                {
                    currRespose = country.wrongAnswer + "Du hast so viel getippt " + country.currNR + " eigentlich ist es aber auf dem Platz" + country.rightNR + country.name;

                }
                phoneChat.SpawnTextBox(currRespose, false);
            }

        }

        intA++;

    }
    public void OnClickOpenMainMenu()
    {
        LevelManager.instance.ActivateMainMenu();
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void AnswerManagerVoidCall(string content)
    {
        if (intA <= 6)
        {
            Debug.Log("unter 7 antworten");
         //  StartCoroutine(AnswerManagerVoid(content));
        }
        else
        {
            Debug.Log("Über 6 antwortem");
          //  StartCoroutine(AnswerManagerVoid("end"));
        }
    }
    //IEnumerator AnswerManagerVoid(string answertype)
    //{

    //    yield return new WaitForSecondsRealtime(.7f);

    //    if (answertype == "moreInfo")
    //    {
    //        if (intA == 0)
    //        {
    //            StartCoroutine(AnswerManagerVoid("solution"));
    //        }
    //        else
    //        {
    //            phoneChat.TypePlayerChat();
    //            yield return new WaitForSecondsRealtime(.7f);

    //            phoneChat.SpawnTyping();
    //            yield return new WaitForSecondsRealtime(.7f);
    //            phoneChat.SpawnTextBox(moreInfo[intA - 2], false);
    //        }
    //        yield return new WaitForSecondsRealtime(.7f);
    //        phoneChat.EnableChatBox();

    //    }

    //    else if (answertype == "solution")
    //    {

    //        phoneChat.TypePlayerChat();
    //        yield return new WaitForSecondsRealtime(.7f);
    //        phoneChat.SpawnTyping();
    //        yield return new WaitForSecondsRealtime(.7f);
    //        CompareSolutions();
    //        // yield return new WaitForSecondsRealtime(.7f);
    //        //phoneChat.SpawnImageBox(images[intA - 1]);
    //        yield return new WaitForSecondsRealtime(.7f);

    //        Debug.Log("solutions");
    //        phoneChat.EnableChatBox();


    //    }

    //    else if (answertype == "begin")
    //    {
    //        restartButton.SetActive(false);
    //        phoneChat.SpawnTyping();
    //        yield return new WaitForSecondsRealtime(.7f);
    //        phoneChat.SpawnTextBox(beginAnswer, false);
    //        yield return new WaitForSecondsRealtime(.7f);

    //        phoneChat.EnableChatBox();

    //    }
    //    else if ((answertype == "end"))
    //    {
    //        phoneChat.TypePlayerChat();
    //        yield return new WaitForSecondsRealtime(.7f);
    //        phoneChat.SpawnTyping();
    //        yield return new WaitForSecondsRealtime(.7f);
    //        phoneChat.SpawnTextBox(endAnswer, false);


    //        // LevelManager.instance.UpdatButtons(GameObject.FindObjectOfType<LevelUpdate>().level);

    // }


    //}
}
