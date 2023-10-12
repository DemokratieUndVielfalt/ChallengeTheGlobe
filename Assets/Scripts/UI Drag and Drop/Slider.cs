using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    bool isOut = true;
    Vector3 oGPos;
    [SerializeField] Vector3 newPos;
    [SerializeField] float speed = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        oGPos = transform.position;
     //   MoveSlider();
    }
    public void MoveSlider()
    {
        StartCoroutine(MoveToTarget());
    }
    IEnumerator MoveToTarget()
    {
        if (isOut == true)
        {

            transform.position = newPos;
            yield return null;

            isOut = false;

        }
        else
        {

            transform.position = oGPos;
            yield return null;

            isOut = true;

        }

    }
}
