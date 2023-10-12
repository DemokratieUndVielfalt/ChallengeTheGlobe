using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinentManager : MonoBehaviour
{

    public SlotManager slotmananger;

    public string continentName;
    [SerializeField] TextMeshProUGUI continentNameText;

    [Header("Antowrten auf PlayerInput: Ausfüllen")]

    [SerializeField] TextMeshProUGUI visibleNumber;
    [SerializeField] TextMeshProUGUI visibleNumberMultiplikator;
    GameObject visibenumberObject;
    GameObject visibenumberMultiplikatorObject;

    ResponsesToSolution responses;

    float multiplikator;
    string multplikatorNR;
    private void Start()
    {
        responses = GameObject.FindObjectOfType<ResponsesToSolution>();
        continentNameText.text = continentName;
        visibenumberObject = visibleNumber.gameObject;
        visibenumberMultiplikatorObject = visibleNumberMultiplikator.gameObject;
        slotmananger = GetComponentInChildren<SlotManager>();
        visibleNumber.text = 0.ToString();
        visibleNumberMultiplikator.text = 0.ToString();
        multiplikator = responses.mulitplikator;
        multplikatorNR = responses.mulitplikatorZahl;
        UpdateText();
    }
    public void UpdateText()
    {
        float currNR = slotmananger.currSlot;
        visibleNumber.text = currNR.ToString();
        LeanTween.scale(visibenumberObject, Vector3.one * 1.4f, .3f).setEase(LeanTweenType.punch);

        string currNRM = (slotmananger.currSlot * multiplikator).ToString() + " " + multplikatorNR;
        visibleNumberMultiplikator.text = currNRM;
        LeanTween.scale(visibenumberMultiplikatorObject, Vector3.one * 1.4f, .3f).setEase(LeanTweenType.punch);
    }

}
