using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DiceRoll : MonoBehaviour
{
    public float throwForce = 10.0f;


    bool isGrounded = false;
    bool checkNumber = false;

    Vector3 initPos;

    [SerializeField] int diceNumber;
    private Rigidbody rb;

    public int rollNR = 0;

    bool sleepingAnim = true;
    [SerializeField] List<DiceSideCheck> diceSides = new List<DiceSideCheck>();
    [SerializeField] List<int> numbersDice = new List<int>();
    [SerializeField] List<Material> numberMaterials = new List<Material>();

    [SerializeField] AnimationCurve animc;
    public GameObject feedbackObject;

    DiceSideCheck currSide;
    void Start()
    {
        // feedbackObject.SetActive(false);
        int a = 0;
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        rb.useGravity = false;
        for (int i = 0; i < 6; i++)
        {
            a++;
            numbersDice.Add(a);
        }
        feedbackObject.SetActive(true);
        LeanTween.rotate(feedbackObject, new Vector3(0, 100, 0), 1f).setLoopType(LeanTweenType.easeOutQuad).setEase(animc);
        LeanTween.scale(feedbackObject, feedbackObject.transform.localScale * 1.03f, .8f).setEase(LeanTweenType.punch);
        Vector3 aa = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        feedbackObject.transform.position = aa;
    }
    private void Update()
    {

    }
    public void ThrowDiceUpdate(int currNR)
    {
        LeanTween.cancel(feedbackObject);
        feedbackObject.SetActive(false);
        Debug.Log("aus");
        sleepingAnim = true;
        if (isGrounded == false && InputManagerDice.instance.canAssign == false)
        {
            checkNumber = true;
            ThrowDice(currNR);

            LeanTween.cancel(gameObject);
        }

        if (rb.IsSleeping())
        {
            isGrounded = false;
            if (checkNumber == true)
            {
                checkNumber = false;
                SideValueCheck();
                feedbackObject.transform.position = currSide.transform.position;
                LeanTween.rotate(feedbackObject, new Vector3(0, 100, 0), 1f).setLoopType(LeanTweenType.easeOutQuad).setEase(animc);
                feedbackObject.SetActive(true);
                LeanTween.scale(feedbackObject, feedbackObject.transform.localScale * 1.3f, .8f).setEase(LeanTweenType.punch);

            }
        }
        else
        {
            isGrounded = true;
        }



    }

    void ThrowDice(int urrNr)
    {
        InputManagerDice.instance.uiDice.UpdateTipsSelect();
        if (urrNr != 0)
        {
            ReplaceSide(urrNr);

        }
        transform.position = initPos;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 200), Random.Range(0, 200), Random.Range(0, 200));
        rb.AddForce(Random.Range(-1500, 1500), Random.Range(-1500, 1500), Random.Range(-1500, 1500));
        diceNumber = 0;
        rollNR++;
    }
    void SideValueCheck()
    {
        foreach (DiceSideCheck side in diceSides)
        {
            if (side.OnGround())
            {

                diceNumber = side.sideValue;
                currSide = side;
            }
        }
        InputManagerDice.instance.canAssign = true;
        InputManagerDice.instance.currNR = diceNumber;
    }
    public void ReplaceSide(int currDiceNR)
    {
        int randamA;

        if (rollNR == 5)
        {
            randamA = 0;
        }
        else if (rollNR == 6)
        {
            return;
        }
        else
        {
            numbersDice.RemoveAt(numbersDice.IndexOf(currDiceNR));
            randamA = numbersDice[Random.Range(1, numbersDice.Count)];
        }
        foreach (DiceSideCheck side in diceSides)
        {
            if (rollNR != 5)
            {
                if (side.sideValue == currDiceNR)
                {
                    // Debug.Log("new value for" + side.sideValue + randamA);
                    side.sideValue = randamA;
                    side.numberObject.GetComponent<Renderer>().material = numberMaterials[randamA - 1];
                }
            }
            else
            {
                side.sideValue = numbersDice[0];
                side.numberObject.GetComponent<Renderer>().material = numberMaterials[numbersDice[0] - 1];
            }
        }
    }
}
