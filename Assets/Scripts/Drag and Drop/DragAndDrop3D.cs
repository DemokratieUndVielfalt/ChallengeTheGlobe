using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop3D : MonoBehaviour
{
    private bool isBeingHeld = false;
    private Transform pickedObject;
    private Vector3 mousePos;

    public string targetTag = "Slot";
    public Transform dragPlane;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f) && hit.transform.tag == targetTag)
            {
                pickedObject = hit.transform;
                isBeingHeld = true;
                mousePos = hit.point - dragPlane.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingHeld = false;
        }

        if (isBeingHeld)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.x = dragPlane.position.x;
            newPos.z = dragPlane.position.z;
            newPos.y = mousePos.y + dragPlane.position.y;
            pickedObject.position = newPos;
        }
    }
}
