using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, -450, 0);
    }

    public void OnClickCreditMenu()
    {
        SoundManager.instance.HandyUpSound();
      //  SoundManager.instance.PlayButtonSound();
        if (open)
        {
            open = false;
            LeanTween.moveLocalY(gameObject, -450, 1f).setEase(LeanTweenType.easeInOutBack);
        }
        else
        {
            open = true;
            LeanTween.moveLocalY(gameObject, -40, 1f).setEase(LeanTweenType.easeInOutBack);

        }
    }
}
