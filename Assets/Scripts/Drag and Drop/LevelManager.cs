using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> level = new List<GameObject>();

    int currLevel = 1;

    [SerializeField] GameObject mainMenu;
    [SerializeField] List<Button> levelButtons = new List<Button>();
    [SerializeField] List<bool> levelOpen = new List<bool>();

    [SerializeField] GameObject fadeBlack;

    public List<List<int>> previousLevelSlots = new List<List<int>>();



    public static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public List<GameObject> allMessages = new List<GameObject>();

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        for (int i = 0; i < level.Count; i++)
        {
            previousLevelSlots.Add(level[i].GetComponent<LevelUpdate>().responses.continentsRightAnswer);
        }
        UpdatButtons(0);
    }

    public void OpenCurrLevel()
    {
        //if (currLevel != 5)
        //{

        for (int i = 0; i < level.Count; i++)
        {
            if (i != currLevel - 1)
            {
                level[i].SetActive(false);
            }
            else
            {
                level[i].SetActive(true);

            }
        }
        //}
        //else
        //{
        //    foreach (GameObject level in level)
        //    {
        //        level.SetActive(false);
        //    }

        //   // OpenDiceMenu();
        //}
        DeactivateMainManu();

    }
    public void UpdateNextLevel(int currlvl)
    {
        UpdatButtons(currlvl - 1);

    }
    public void OpenNextLevel(int currlvl)
    {
        currLevel = currlvl;
        StartCoroutine(FadeToBlack());

    }
    public void Level1()
    {
        currLevel = 1;
        StartCoroutine(FadeToBlack());
    }
    public void Level2()
    {
        currLevel = 2;
        StartCoroutine(FadeToBlack());
    }
    public void Level3()
    {
        currLevel = 3;
        StartCoroutine(FadeToBlack());
    }
    public void Level4()
    {
        currLevel = 4;
        StartCoroutine(FadeToBlack());
    }
    public void Level5()
    {
        currLevel = 5;
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        SoundManager.instance.PlayFadeToBlack();
        SoundManager.instance.PlayButtonSound();
        yield return new WaitForSecondsRealtime(.2f);
        LeanTween.alpha(fadeBlack.GetComponent<RectTransform>(), 1f, .6f).setOnComplete(OpenCurrLevel);
        LeanTween.alpha(fadeBlack.GetComponent<RectTransform>(), 0.0001f, .6f).setDelay(.8f);

    }
    void DeactivateMainManu()
    {
        mainMenu.SetActive(false);
    }
    public void ActivateMainMenu()
    {
        LeanTween.alpha(fadeBlack.GetComponent<RectTransform>(), 1f, .2f).setOnComplete(ActivateMainMenu2);
        LeanTween.alpha(fadeBlack.GetComponent<RectTransform>(), 0.0001f, .5f).setDelay(.5f);
    }
    void ActivateMainMenu2()
    {
        mainMenu.SetActive(true);

    }

    public void UpdatButtons(int unlockLevel)
    {
        levelOpen[unlockLevel] = true;

        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (levelOpen[i])
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;

            }
        }

    }
    public void UpdateMessageList(GameObject message, ChatBox messageChatBox)
    {
        messageChatBox.enabled = false;
        allMessages.Add(message);
       // allMessageLengh++;
    }


}
