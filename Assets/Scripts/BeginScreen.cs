using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BeginScreen : MonoBehaviour
{
    [SerializeField] Texture chatOn;
    [SerializeField] Texture chatOff;
    [SerializeField] RawImage chatButton;
    [SerializeField] CanvasGroup waves;
    bool chat;

    private void Start()
    {
        PlayerPrefs.SetInt("Chat", 1);

        Debug.Log("start");
        //LeanTween.moveLocalY(gameObject, -1000, 0.01f).setEase(LeanTweenType.easeInOutBack);
        //LeanTween.moveLocalY(gameObject, 0, .3f).setEase(LeanTweenType.easeInOutBack);

        //Time.timeScale = 0/*;*/
        //Debug.Log(Time.timeScale);
        //LeanTween.alphaCanvas(waves, 0, 0.2f).setEase(LeanTweenType.easeInOutBack);

    }
    public void OnClickChatButton()
    {
        if (chat)
        {
            chatButton.texture = chatOff;
            chat = false;
        }
        else
        {
            chatButton.texture = chatOn;
            chat = true;

        }
    }
    public void OnClickContinue()
    {
        //Time.timeScale = 1;
        if (chat)
        {
            PlayerPrefs.SetInt("Chat", 1);

        }
        else
        {
            PlayerPrefs.SetInt("Chat", 0);
        }
        //LeanTween.value(0, 1, 0.2f).
        //LeanTween.moveY(waves, 1000, 2f).setOnComplete(LoadScene);
        //LeanTween.alphaCanvas(waves, 1, 0.5f).setEase(LeanTweenType.easeInOutBack).setOnComplete(LoadScene);
        //LeanTween.moveLocalY(waves.gameObject, waves.gameObject.transform.localPosition.y + 100, 0.3f).setEase(LeanTweenType.easeInOutBack);

        PlayerPrefs.Save();
        LoadScene();
        //LevelManager.instance.noPhone = chat;

    }
    void LoadScene()
    {

        SceneManager.LoadScene(1);
    }
}
