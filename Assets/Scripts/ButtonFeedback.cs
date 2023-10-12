using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFeedback : MonoBehaviour
{
    [SerializeField] GameObject feedbackObject;
    [SerializeField] float strengh;
    //private void Start()
    //{
     
    //   // FadeIn();
    //}
    private void OnValidate()
    {
        if (feedbackObject == null)
        {
            feedbackObject = this.gameObject;
        }
        if (strengh == 0)
        {
            strengh = 1.2f;
        }
    }

    void FadeIn()
    {
        LeanTween.scale(feedbackObject, Vector3.one * strengh, .3f).setEasePunch();

    }
    public void OnClickFeedback()
    {
        LeanTween.scale(feedbackObject, Vector3.one * strengh, .3f).setEasePunch();
    }
    void OnCLickFadeOut()
    {

    }
    private void OnEnable()
    {
        FadeIn();
    }
}

