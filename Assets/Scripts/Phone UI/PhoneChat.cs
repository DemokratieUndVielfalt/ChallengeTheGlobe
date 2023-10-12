using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneChat : MonoBehaviour
{
    //[Header("Chat Message Data")]
    //[SerializeField] float scale;
    //[SerializeField] float textScale;
    [Header("UI Prefabs")]
    [SerializeField] GameObject nPCchatBoxPrefab;
    [SerializeField] GameObject playerchatBoxPrefab;
    [SerializeField] GameObject imageBoxPrefab;
    public GameObject newMessage;

    [Header("UI Assign")]
    [SerializeField] GameObject objectsToHide;
    [SerializeField] GameObject parentObject;
    [SerializeField] GameObject solutionButton;
    [SerializeField] GameObject solutionScreen;
    [SerializeField] GameObject chatBoxAnswer;
    [SerializeField] GameObject blackFade;
    [SerializeField] GameObject hidePhoneButton;
    [SerializeField] ScrollRect scrollView;
    [SerializeField] GameObject solutionCaption;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject restartButton;
    TextMeshProUGUI hidePhoneButtonText;

    GameObject currTyping;

    [Header("Script Assign")]

    [SerializeField] GameObject inputManagerObject;
    OnlyVountrieInfoInput onlyCountryInput;
    InputManager inputManager;
    SolutionUI solutionUI;
    ResponsesToSolution responses;
    bool phoneHidden = true;
    // [SerializeField] bool diceGame;

    Introduction introduction;

    int box1Answer = 1;
    int box2Answer = 2;
    public int boxCurr = 1;
    [HideInInspector] public bool phoneLeft;
    [SerializeField] TextMeshProUGUI button1;
    [SerializeField] TextMeshProUGUI button2;

    [SerializeField] Color32 color;

    public bool intro = true;
    bool end = false;
    bool isTransitioning = false;

    int messageNr = 0;
    [SerializeField] bool menuLvl;
    private void OnEnable()
    {
        // Debug.Log(messageNr+"message nr");
        if (menuLvl)
        {
            menuLvl = false;
        }
        else
        {
            UpdateMessage();
        }
    }
    void UpdateMessage()
    {

        int lengh = LevelManager.instance.allMessages.Count;
        if (messageNr < lengh)
        {
            for (int i = messageNr; i < lengh; i++)
            {
                GameObject message = Instantiate(LevelManager.instance.allMessages[i]);
                message.transform.SetParent(parentObject.transform, false);
                messageNr++;
            }
        }
    }
    private void Start()
    {
        UpdateMessage();

        DisableChatBox();

        inputManager = inputManagerObject.GetComponent<InputManager>();
        onlyCountryInput = inputManagerObject.GetComponent<OnlyVountrieInfoInput>();
        onlyCountryInput.enabled = false;
        hidePhoneButtonText = hidePhoneButton.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        solutionUI = GetComponent<SolutionUI>();
        responses = GetComponent<ResponsesToSolution>();
        newMessage.SetActive(false);
        if (intro)
        {
            // solutionButton.GetComponent<Button>().interactable = false;
            LeanTween.moveLocalY(objectsToHide, 520, 0.01f).setEase(LeanTweenType.easeInOutBack);
            onlyCountryInput.enabled = false;
            inputManager.enabled = false;
            // inputManagerObject.SetActive(false);
            phoneHidden = false;

            StartCoroutine(WaitBeforePhone());
        }
        else
        {
            solutionScreen.SetActive(false);
        }
    }

    IEnumerator WaitBeforePhone()
    {

        hidePhoneButton.SetActive(false);
        yield return new WaitForSecondsRealtime(1f);

        solutionUI.AnswerManagerVoidCall(1);

        OnClickHidePhone();

    }
    public void SpawnTextBox(string content, bool player)
    {
        if (player)
        {
            GameObject chatBox = Instantiate(playerchatBoxPrefab);
            ChatBox chatboxComponent = chatBox.GetComponent<ChatBox>();
            chatboxComponent.updateContent(content, parentObject);
            if (content == "Ok weiter zum Spiel")
            {
                //  WaitBeforePhone();
                OnClickHidePhone();
            }
            LevelManager.instance.UpdateMessageList(chatBox, chatboxComponent);
        }
        else
        {
            SoundManager.instance.PlayMessageSound();
            Destroy(currTyping);
            GameObject chatBox = Instantiate(nPCchatBoxPrefab);
            ChatBox chatboxComponent = chatBox.GetComponent<ChatBox>();
            chatboxComponent.updateContent(content, parentObject);
            chatboxComponent.UpdateNPC(responses.npcInt[solutionUI.intA]);

            LevelManager.instance.UpdateMessageList(chatBox, chatboxComponent);

        }
        messageNr++;
        ScrollDown();
    }
    public void SpawnImageBox(Texture texture)
    {
        SoundManager.instance.PlayMessageSound();

        GameObject imageBox = Instantiate(imageBoxPrefab);
        ChatBox imageBoxComponent = imageBox.GetComponent<ChatBox>();
        imageBoxComponent.updateImage(parentObject, texture);
        imageBoxComponent.GetComponent<ChatBox>().UpdateNPC(responses.npcInt[solutionUI.intA]);

        LevelManager.instance.UpdateMessageList(imageBox, imageBoxComponent);
        messageNr++;
        ScrollDown();
    }
    public void TypePlayerChat(string content)
    {
        SpawnTextBox(content, true);
        ScrollDown();
    }
    public void OnClickHidePhone()
    {
        if (!isTransitioning)
        {
            SoundManager.instance.HandyUpSound();
            isTransitioning = true;
            blackFade.SetActive(true);

            LeanTween.scale(hidePhoneButton, hidePhoneButton.transform.localScale * 0.5f, .3f).setEaseInBack().setOnComplete(HidePhoneButton);
            if (phoneHidden)
            {
                LeanTween.alpha(blackFade.GetComponent<RectTransform>(), .9f, .1f).setOnComplete(SoundManager.instance.PlayPhoneOnOff).setDelay(.2f);


                LeanTween.moveLocalY(objectsToHide, 520, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(SetTrantitionFalse).setDelay(.4f);

                phoneHidden = false;
                inputManager.enabled = false;
                onlyCountryInput.enabled = false;


                hidePhoneButtonText.text = "Handy?";
                if (!end)
                {
                    //    Debug.Log("an");
                    inputManager.enabled = true;
                }
                else
                {

                    onlyCountryInput.enabled = true;
                }


            }
            else
            {
                LeanTween.moveLocalY(objectsToHide, 25, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(SetTrantitionFalse).setDelay(0f);
                LeanTween.alpha(blackFade.GetComponent<RectTransform>(), 0.0001f, .1f).setOnComplete(SoundManager.instance.PlayPhoneOnOff).setDelay(.6f);
                phoneHidden = true;
                hidePhoneButtonText.text = "Handy!";

                //    onlyCountryInput.enabled = true;

                // Debug.Log("an");
                // Debug.Log("aus");
                onlyCountryInput.enabled = false;
                inputManager.enabled = false;

                // inputManager.enabled = false;
                newMessage.SetActive(false);
                //    inputManagerObject.SetActive(false);

            }
        }
    }

    public void MovePhoneToLeft()
    {
        LeanTween.moveLocalX(objectsToHide, -322, 1f).setEase(LeanTweenType.easeInOutBack).setDelay(.2f);
        SoundManager.instance.HandyUpSound();
    }
    void HidePhoneButton()
    {
        hidePhoneButton.SetActive(false);
        hidePhoneButton.transform.localScale = Vector3.one;

    }
    public void SetTrantitionFalse()
    {
        isTransitioning = false;
        hidePhoneButton.SetActive(true);
        LeanTween.scale(hidePhoneButton, hidePhoneButton.transform.localScale * 1.16f, .8f).setEase(LeanTweenType.easeOutExpo).setEasePunch();

    }
    public void OnClickSolutionCheck()
    {
        ButtonSound();
        intro = false;
        end = true;
        solutionUI.intA = responses.introOver - 1;
        //Debug.Log("inta" + solutionUI.intA);
        solutionUI.auflösung = true;
        solutionButton.SetActive(false);
        solutionScreen.SetActive(true);

        phoneHidden = false;
        OnClickHidePhone();


        solutionUI.CompareSolutions();
        solutionUI.AnswerManagerVoidCall(1);


    }
    public void OnClickChatBox1()
    {
        ButtonSound();
        DisableChatBox();

        boxCurr = box1Answer;

        solutionUI.AnswerManagerVoidCall(1);


    }
    public void OnClickChatBox2()
    {
        // Debug.Log("zweite box");
        ButtonSound();
        DisableChatBox();

        boxCurr = box2Answer;

        solutionUI.AnswerManagerVoidCall(2);

    }

    public void ButtonSound()
    {
        SoundManager.instance.PlayButtonSound();
    }

    public void EnableChatBox()
    {
        if (responses.button1ListContent.Count > solutionUI.intA)
        {
            string button1content = responses.button1ListButton[solutionUI.intA];
            string button2content = responses.button2ListButton[solutionUI.intA];
            if (button1content == "0")
            {
                ChatBox1State(false);
            }
            else
            {
                ChatBox1State(true);
            }


            if (button2content == "0")
            {
                ChatBox2State(false);
            }
            else
            {
                ChatBox2State(true);
            }

            button1.text = button1content;
            button2.text = button2content;
        }
        else
        {
            ChatBox2State(false);
            ChatBox1State(false);
        }
        LeanTween.moveLocalY(chatBoxAnswer, 0, 0.3f).setEase(LeanTweenType.easeInOutBack);

    }
    public void DisableChatBox()
    {
        // Debug.Log("disable");
        LeanTween.moveLocalY(chatBoxAnswer, -70, 0.3f).setEase(LeanTweenType.easeInOutBack);
    }

    void ChatBox1State(bool state)
    {
        if (state)
        {
            chatBoxAnswer.transform.GetChild(0).gameObject.SetActive(true);

        }
        else
        {
            chatBoxAnswer.transform.GetChild(0).gameObject.SetActive(false);

        }
    }
    void ChatBox2State(bool state)
    {
        if (state)
        {
            chatBoxAnswer.transform.GetChild(1).gameObject.SetActive(true);

        }
        else
        {
            chatBoxAnswer.transform.GetChild(1).gameObject.SetActive(false);

        }
    }
    void ScrollDown()
    {
        LeanTween.value(scrollView.verticalNormalizedPosition, -1, .2f).setOnUpdate(ScrollDownUpdate);
    }
    void ScrollDownUpdate(float nr)
    {
        scrollView.verticalNormalizedPosition = nr;

    }
    public void EnableSolutionCheckButton(bool enable)
    {
        if (enable)
        {
            //solutionButton.SetActive(true);
            solutionButton.GetComponent<Button>().interactable = true;
            LeanTween.scale(solutionButton, Vector3.one * 1.44f, 1f).setEasePunch();

        }
        else
        {
            //solutionButton.SetActive(false);
            solutionButton.GetComponent<Button>().interactable = false;


        }
    }
    public void SpawnTyping()
    {
        currTyping = Instantiate(nPCchatBoxPrefab);
        ChatBox currTypingChatbox = currTyping.GetComponent<ChatBox>();
        currTypingChatbox.updateContent("...", parentObject);
        currTypingChatbox.UpdateNPC(responses.npcInt[solutionUI.intA]);
        ScrollDown();
    }
    public void SolutionScreen()
    {
        solutionCaption.SetActive(true);
        solutionCaption.GetComponent<CanvasGroup>().alpha = 0;
        LeanTween.alphaCanvas(solutionCaption.GetComponent<CanvasGroup>(), 1, 1.2f).setEaseInOutExpo();
    }
    public void OnClickRestartLevel()
    {
        solutionButton.GetComponent<Button>().interactable = false;
        restartButton.GetComponent<Button>().interactable = false;
        foreach (SlotManager slotManager in FindObjectsOfType<SlotManager>())
        {
            for (int i = 0; i < 11; i++)
            {
                if (slotManager.currSlot != 0)
                {

                    GlobalSlotManager.instance.GetFromInventoryRestart(slotManager);
                    Debug.Log(slotManager.currSlot);
                    // slotManager.currSlot--;
                }
                else
                {
                    Debug.Log("mach nichts");
                }




            }

            GlobalSlotManager.instance.Restart();

        }
        StartCoroutine(WaitBeforeEnablingRestart());
        //  GlobalSlotManager.instance.Restart();
    }
    IEnumerator WaitBeforeEnablingRestart()
    {
        yield return new WaitForSecondsRealtime(1);
        restartButton.GetComponent<Button>().interactable = true;
    }
    public void MenuButtonActive(bool active)
    {
      
        if (active)
        {
            mainMenuButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            restartButton.GetComponent<Button>().interactable = false;
            mainMenuButton.GetComponent<Button>().interactable = false;
        }
    }
}
