using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Vector3 oGPos;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        oGPos = transform.position;
        //   transform.localScale = Vector3.one*.7f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ColliderStop")
        {
            //  transform.position = oGPos;
            GlobalSlotManager.instance.AddBackToList(gameObject);
            rb.isKinematic = true;
            GlobalSlotManager.instance.inputManager.draggedObject = null;
            rb.isKinematic = false;
            LeanTween.scale(gameObject, Vector3.one * 1.3f, 0.3f).setEasePunch();
            //  LeanTween.scale(gameObject, Vector3.one * 1f, 0.5f).setEase(LeanTweenType.easeOutElastic).setDelay(.05f);

        }

    }
    public void SetPosition()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        //  transform.position = oGPos;
        gameObject.SetActive(false);

    }


}
