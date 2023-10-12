using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager2 : MonoBehaviour
{
    private GameObject selectedGameObject;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedGameObject == null)
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Slot"))
                    {
                        return;
                    }
                    selectedGameObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            else
            {

            }
        }
        if (selectedGameObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedGameObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedGameObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
        }
    }
    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
           Input.mousePosition.x,
           Input.mousePosition.y,
           Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

}
