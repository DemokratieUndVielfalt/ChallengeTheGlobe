using UnityEngine;

public class Grabber : MonoBehaviour
{

    private GameObject selectedObject;
    bool dragging;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dragging == false)
            {
                RaycastHit[] hits = CastRay();
                foreach (RaycastHit hit in hits)
                {

                    if (hit.collider.CompareTag("Slot"))
                    {
                        selectedObject = hit.collider.gameObject;
                        dragging = true;
                        Cursor.visible = false;
                    }
                }


            }

        }
        if (Input.GetMouseButtonUp(0))
        {

            selectedObject = null;
            Cursor.visible = true;
            dragging = false;
        }

        if (dragging == true)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            selectedObject.transform.position = new Vector3(worldPosition.x, -100000, worldPosition.z);

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
