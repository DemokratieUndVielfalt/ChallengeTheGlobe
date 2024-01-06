using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMenuActivate : MonoBehaviour
{
    int a = 0;
    // Start is called before the first frame update
    public void SetMenuActiveIfComplete()
    {
        a++;
        //Debug.Log(a);
        if (a == 5)
        {
            GameObject.FindObjectOfType<PhoneChat>().MenuButtonActive(true);
            a = 0;
        }
    }

}
