using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputManagerDice : MonoBehaviour
{

    public static InputManagerDice instance;
    public static InputManagerDice Instance { get { return instance; } }
    public bool canAssign = false;
    public int currNR;
    Country currCountry;
    [SerializeField] PhoneChat phoneChat;

    DiceRoll diceRoll;
    GameObject diceRollObject;
    [SerializeField] GameObject dicePrefab;
    Vector3 dicePos = new Vector3(0.1654f, -36.630f, 0.04251f);
    public UIDice uiDice;
    [SerializeField] GameObject feedbackObject;
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
        Restart();
    }
    public void Restart()
    {
        if (diceRollObject != null)
        {
            currNR = 0;
            Destroy(diceRollObject);
        }
        phoneChat.EnableSolutionCheckButton(false);

        diceRollObject = Instantiate(dicePrefab, transform.position, transform.rotation);
        diceRollObject.transform.SetParent(gameObject.transform);
        diceRoll = diceRollObject.GetComponent<DiceRoll>();
        diceRoll.feedbackObject = feedbackObject;

        foreach (Country country in FindObjectsOfType<Country>())
        {
            country.Restart();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (diceRollObject != null)
            {

                diceRoll.ThrowDiceUpdate(currNR);
            }
            RaycastHit[] hits = CastRay();
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Continent"))
                {

                    if (hit.collider.gameObject.GetComponent<Country>().free == true)
                    {
                        if (canAssign == true)
                        {

                            if (diceRoll.rollNR == 6)
                            {
                                Debug.Log("enable");
                                Destroy(diceRollObject);
                                feedbackObject.SetActive(false);
                                phoneChat.EnableSolutionCheckButton(true);
                                currCountry = hit.collider.gameObject.GetComponent<Country>();
                                UpdateContinentNumber();

                            }
                            else if (diceRoll.rollNR == 7)
                            {
                                return;
                            }
                            else
                            {
                                currCountry = hit.collider.gameObject.GetComponent<Country>();
                                UpdateContinentNumber();
                            }

                        }
                        else
                        {
                            currCountry = null;
                        }
                    }
                }
                else
                {
                    currCountry = null;
                }

            }
        }


    }
    public void UpdateContinentNumber()
    {
        if (currCountry != null)
        {

            canAssign = false;
            currCountry.UpdateNumber(currNR);
            uiDice.UpdateTipsEmpty();

        }
    }
    private RaycastHit[] CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit[] hits = Physics.RaycastAll(worldMousePosNear, worldMousePosFar - worldMousePosNear);

        return hits;
    }
}