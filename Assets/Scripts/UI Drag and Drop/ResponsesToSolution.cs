using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResponsesToSolution : MonoBehaviour
{


    public List<int> continentsRightAnswer = new List<int>();
    //public List<Texture> correspondingImage = new List<Texture>();

    // [SerializeField] List<string> text = new List<string>();

    [Header("Button 1 Player")]
    public List<string> button1ListButton = new List<string>();
    public List<string> button2ListButton = new List<string>();
    public List<string> button1ListContent = new List<string>();
    public List<string> button2ListContent = new List<string>();

    [Header("Button 1 NPC")]
    [Multiline(3)]

    public List<string> npcButton1List = new List<string>();
    public List<Texture> images1Button = new List<Texture>();

    [Header("Button 2 Player")]


    [Header("Button 2 NPC")]
    [Multiline(3)]

    public List<string> npcButton2List = new List<string>();
    public List<Texture> images2Button = new List<Texture>();
    public List<int> npcInt = new List<int>();

    [Header("Ab wann die Introduction Zuende ist")]
    public int introOver;

    [Header("Zuweisung der Zahlen pro Kontinent")]

    [Range(0, 12)] [SerializeField] int afrikaAnswer;
    [Range(0, 12)] [SerializeField] int australienAsienAnswer;
    [Range(0, 12)] [SerializeField] int europaAnswer;
    [Range(0, 12)] [SerializeField] int nordamerikaAnswer;
    [Range(0, 12)] [SerializeField] int südamerikaAnswer;

    [Tooltip("Do not exceed 20")]
    [SerializeField] int total;


    public float mulitplikator;
    public string mulitplikatorZahl;

    //public string beginAnswer;
    //public string endAnswer;

    // public string checkSolutions;
    //public List<string> finalAnswers = new List<string>();
    //public List<string> moreInfo = new List<string>();

    private void OnValidate()
    {
        total = afrikaAnswer + australienAsienAnswer + europaAnswer + nordamerikaAnswer + südamerikaAnswer;
        continentsRightAnswer.Clear();
        continentsRightAnswer.Add(afrikaAnswer);
        continentsRightAnswer.Add(australienAsienAnswer);
        continentsRightAnswer.Add(europaAnswer);
        continentsRightAnswer.Add(südamerikaAnswer);
        continentsRightAnswer.Add(nordamerikaAnswer);
    }


}



