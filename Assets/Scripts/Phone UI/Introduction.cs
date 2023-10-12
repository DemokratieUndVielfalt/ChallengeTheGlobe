using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [Tooltip("Von 1 abwärts")]
    [SerializeField] List<string> tutorial = new List<string>();
    [SerializeField] List<string> tutorialInfo = new List<string>();
    [SerializeField] List<string> playerOkList = new List<string>();
    [SerializeField] List<string> playerOkListButton = new List<string>();
    [SerializeField] List<string> playerWiesoList = new List<string>();
    [SerializeField] List<string> playerWiesoListButton = new List<string>();
    [SerializeField] List<Texture> images = new List<Texture>();
    PhoneChat phoneChat;
    public int intA = -1;
    private void Start()
    {
        phoneChat = GetComponent<PhoneChat>();
    }

    public void SpawnIntroduction(string content, int textBox)
    {
        if (intA == -1)
        {
            intA++;
            StartCoroutine(AnswerManagerVoid("begin"));

        }
        else if (intA < tutorial.Count - 2)
        {


            if (textBox == 1)
            {
                intA++;
                StartCoroutine(AnswerManagerVoid("dialog"));


            }
            else
            {

                StartCoroutine(AnswerManagerVoid("info"));
            }
        }
        else
        {
            if (textBox == 1)
            {

                //  Debug.Log("end");
                intA++;

                StartCoroutine(AnswerManagerVoid("end"));
            }
            else
            {
                StartCoroutine(AnswerManagerVoid("info"));

            }

        }



        //else
        //{

        //    StartCoroutine(AnswerManagerVoid("end"));
        //}




        //if (intA == tutorial.Count - 1)
        //{
        //    StartCoroutine(AnswerManagerVoid("end"));

        //}
        //else
        //{

        //    StartCoroutine(AnswerManagerVoid(content));
        //}
    }
    IEnumerator AnswerManagerVoid(string answertype)
    {
        yield return new WaitForSecondsRealtime(.01f);

        string content = "r";
        if (phoneChat.boxCurr == 1)
        {
            content = playerOkList[intA];
        }
        else
        {
            content = playerWiesoList[intA];

        }


        if (answertype == "begin")
        {
            Debug.Log("begin");

            yield return new WaitForSecondsRealtime(2.2f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(tutorial[intA], false);
            yield return new WaitForSecondsRealtime(.7f);
            if (images.Count > 0)
            {
                phoneChat.SpawnImageBox(images[0]);
            }
          //  phoneChat.EnableChatBox();
         //   phoneChat.UpdateButtons(playerOkListButton[intA], playerWiesoListButton[intA]);
        }

        else if (answertype == "dialog")
        {

            yield return new WaitForSecondsRealtime(.1f);
            phoneChat.TypePlayerChat(content);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(tutorial[intA], false);
            yield return new WaitForSecondsRealtime(.7f);
         //   phoneChat.EnableChatBox();
         //   phoneChat.UpdateButtons(playerOkListButton[intA], playerWiesoListButton[intA]);

        }
        else if (answertype == "info")
        {
            yield return new WaitForSecondsRealtime(.1f);
            phoneChat.TypePlayerChat(content);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(tutorialInfo[intA], false);
            yield return new WaitForSecondsRealtime(.7f);
           // phoneChat.EnableChatBox();
          //  phoneChat.UpdateButtons(playerOkListButton[intA], playerWiesoListButton[intA]);

        }
        else if (answertype == "end")
        {
            yield return new WaitForSecondsRealtime(.1f);
            phoneChat.TypePlayerChat(content);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTyping();
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.SpawnTextBox(tutorial[intA], false);
            yield return new WaitForSecondsRealtime(.7f);
            phoneChat.intro = false;
            if (images.Count > 0)
            {
                phoneChat.SpawnImageBox(images[1]);
            }
            yield return new WaitForSecondsRealtime(2f);
       //     phoneChat.OnClickHidePhone();
            Debug.Log("end");


        }
        //intA++;
    }

}
