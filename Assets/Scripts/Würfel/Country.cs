using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Country : CountryGlobal
{
    [SerializeField] Color toChangeColor;
    Color oGColor;

    public int currNR;
    public int rightNR;
    [HideInInspector] public bool free = true;
    public string rightAnswer;
    public string wrongAnswer;
    [SerializeField] TextMeshProUGUI number;
    [SerializeField] SpriteRenderer sprite;

    private void Awake()
    {

        sprite = GetComponent<SpriteRenderer>();
        oGColor = sprite.color;
    }

    public void UpdateNumber(int nr)
    {
        LeanTween.scale(sprite.gameObject, Vector3.one * 1.03f, .8f).setEase(LeanTweenType.punch);
        number.text = nr.ToString();
        number.color = Color.white;
        LeanTween.scale(number.gameObject, Vector3.one * 1.1f, .8f).setEase(LeanTweenType.punch);
        currNR = nr;
        free = false;
        sprite.color = toChangeColor;
    }
    public void Restart()
    {
        sprite.color = oGColor;
        currNR = 0;
        number.text = currNR.ToString();
        number.color = Color.black;

    }
}
