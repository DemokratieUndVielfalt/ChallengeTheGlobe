using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSideCheck : MonoBehaviour
{
    bool isOnGround;
    public int sideValue;
    public GameObject numberObject;

    //GameObject feedbackObject;
    private void Start()
    {
        //feedbackObject = GetComponentInParent<DiceRoll>().feedbackObject;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Slot")
        {
            isOnGround = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Slot")
        {
            isOnGround = false;
        }
    }
    public bool OnGround()
    {
        return isOnGround;
    }
}
