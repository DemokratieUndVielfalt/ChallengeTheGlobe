using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    public GameObject draggedObject;

    public GameObject infoHover;
    public GameObject infoCountries;
    Transform infoHoverTransform;

    private Vector3 mousePos;
    bool infoHoveractive = false;
    bool infoCountriesactive = false;

    Touch touch;
    float timeInInventory = 0f;
    float timeLimit = 100f;
    bool fingerDown;
    Camera cameraMain;


    private void Start()
    {
        infoHoverTransform = infoHover.GetComponent<Transform>();
        infoHover.SetActive(true);
        //  infoCountries.SetActive(false);
        cameraMain = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        //if (Input.touchCount > 0)
        {
            //fingerDown = true;

            //touch = Input.GetTouch(0);
            //if (touch.phase == TouchPhase.Began)
            //{
            //  touch.phase = TouchPhase.Ended; nicht wieder aktivieren
            RaycastHit[] hits = CastRay();
            foreach (RaycastHit hit in hits)
            {
                if (!hit.collider.CompareTag("Inventory"))
                {

                    if (hit.collider.CompareTag("SlotTower"))
                    {

                        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(hit.transform.position).z);
                        Vector3 worldPosition = cameraMain.ScreenToWorldPoint(position);
                        if (GlobalSlotManager.instance.CanBeSpawnedFrom())
                        {
                            GlobalSlotManager.instance.SpawnFromList(worldPosition);
                            LeanTween.scale(draggedObject, Vector3.one * 1.15f, 0.08f).setEase(LeanTweenType.easeOutBounce);
                            draggedObject.GetComponent<Rigidbody>().isKinematic = false;

                            Cursor.visible = false;
                        }
                    }
                    if (hit.collider.CompareTag("Column"))
                    {
                        infoHoveractive = true;
                        PreviousLevelCount levelCount = hit.collider.gameObject.GetComponent<PreviousLevelCount>();
                        infoHover.SetActive(true);
                        mousePos = Input.mousePosition;
                        infoHoverTransform.position = mousePos;
                        SoundManager.instance.InfoHoverOn();
                        infoHover.GetComponent<HoverInfo>().UpdateText(hit.collider.gameObject.GetComponentInParent<Continents>().continentName,
                                                                        levelCount.level1Count, levelCount.level2Count, levelCount.level3Count, levelCount.level4Count);

                        LeanTween.cancel(infoHover);
                        Cursor.visible = false;
                        infoHover.transform.localScale = Vector3.one / 2;
                        LeanTween.scale(infoHover, Vector3.one, .4f).setEase(LeanTweenType.easeOutBack);
                    }
                    if (hit.collider.CompareTag("CountryList"))
                    {
                        infoCountriesactive = true;
                        ListOfCountries listCountries = hit.collider.gameObject.GetComponent<ListOfCountries>();
                        infoCountries.SetActive(true);
                        SoundManager.instance.InfoHoverOn();
                        infoCountries.GetComponent<CountrieHoverInfo>().WriteCountries(listCountries.content);

                        LeanTween.cancel(infoCountries);
                        Cursor.visible = false;
                        infoCountries.transform.localScale = Vector3.one / 2;
                        LeanTween.scale(infoCountries, Vector3.one, .4f).setEase(LeanTweenType.easeOutBack);
                    }

                    if (hit.collider.gameObject.CompareTag("Slot"))
                    {
                        draggedObject = hit.collider.gameObject;
                        LeanTween.scale(draggedObject, Vector3.one * 1.15f, 0.08f).setEase(LeanTweenType.easeOutBounce);
                        draggedObject.GetComponent<Rigidbody>().isKinematic = false;


                        Cursor.visible = false;




                    }
                    touch.phase = TouchPhase.Ended;
                    fingerDown = false;
                }
                else
                {

                    if (draggedObject == null)
                    {


                        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(hit.transform.position).z);
                        Vector3 worldPosition = cameraMain.ScreenToWorldPoint(position);
                        GlobalSlotManager.instance.GetFromInventory(hit.collider.GetComponent<SlotManager>(), worldPosition);
                        if (draggedObject != null)
                        {
                            LeanTween.scale(draggedObject, Vector3.one * 1.15f, 0.08f).setEase(LeanTweenType.easeOutBounce);

                            draggedObject.GetComponent<Rigidbody>().isKinematic = false;
                        }
                        Cursor.visible = false;
                    }




                    //}
                }

            }
        }
        else if (Input.GetMouseButtonUp(0))
        //else if (Input.touchCount == 0)
        {
            //    touch.phase = TouchPhase.Ended;

            if (infoHoveractive)
            {
                LeanTween.scale(infoHover, Vector3.one * .3f, .4f).setEase(LeanTweenType.easeInQuart).setOnComplete(DisableInfoBox);
                SoundManager.instance.InfoHoverOff();

                infoHoveractive = false;
                Cursor.visible = true;

            }
            if (infoCountriesactive == true)
            {
                LeanTween.scale(infoCountries, Vector3.one * .3f, .4f).setEase(LeanTweenType.easeInQuart).setOnComplete(DisableCountryBox);
                SoundManager.instance.InfoHoverOff();

                infoCountriesactive = false;
                Cursor.visible = true;
            }

            if (draggedObject != null)
            {
                LeanTween.scale(draggedObject, Vector3.one * 1f, 0.08f).setEase(LeanTweenType.easeOutBounce);

                RaycastHit[] hits = CastRay();
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Continent"))
                    {
                        if (hit.collider.GetComponent<Continents>().slotmananger.isFull())
                        {
                            GlobalSlotManager.instance.AddBackToList(draggedObject);
                        }
                        else
                        {
                            draggedObject.GetComponent<Rigidbody>().isKinematic = true;
                            GlobalSlotManager.instance.AddToInvntory(hit.collider.gameObject.GetComponent<Continents>().slotmananger, draggedObject);

                        }

                    }
                    if (hit.collider.CompareTag("SlotTower"))
                    {
                        draggedObject.GetComponent<Rigidbody>().isKinematic = true;

                        GlobalSlotManager.instance.AddBackToList(draggedObject);
                        LeanTween.scale(draggedObject, Vector3.one * 1.3f, .3f).setEasePunch();
                    }

                }
            }

            draggedObject = null;
            Cursor.visible = true;
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    mousePos = Input.mousePosition - GetMousePos();
        //    RaycastHit[] hits = CastRay();
        //    foreach (RaycastHit hit in hits)
        //    {


        //        if (hit.collider.gameObject.CompareTag("Inventory"))
        //        {
        //            if (draggedObject == null)
        //            {
        //                timeInInventory += Time.deltaTime; // Increment the timer by the time since the last frame
        //                if (timeInInventory >= timeLimit) // Check if the time limit has been reached
        //                {
        //                    // Execute some code here
        //                    Debug.Log("Time limit reached!");
        //                    timeInInventory = 0f; // Reset the timer
        //                }
        //                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(hit.transform.position).z);
        //                Vector3 worldPosition = cameraMain.ScreenToWorldPoint(position);
        //                GlobalSlotManager.instance.GetFromInventory(hit.collider.GetComponent<SlotManager>(), worldPosition);
        //            }
        //            else
        //            {
        //                timeInInventory = 0f; // Reset the timer if the condition is not true
        //                Debug.Log("fff");
        //            }
        //        }
        //        else
        //        {
        //            timeInInventory = 0f; // Reset the timer if the condition is not true
        //        }
        //    }
        //}
        if (draggedObject != null)
        {

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraMain.WorldToScreenPoint(draggedObject.transform.position).z);
            Vector3 worldPosition = cameraMain.ScreenToWorldPoint(position);

            draggedObject.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);

            draggedObject.GetComponent<Rigidbody>().AddForce(cameraMain.transform.right * Input.GetAxis("Mouse X") * 1f, ForceMode.Impulse);
            draggedObject.GetComponent<Rigidbody>().AddForce(cameraMain.transform.up * Input.GetAxis("Mouse Y") * 1f, ForceMode.Impulse);
        }

    }

    void GetFromInventory(RaycastHit hit)

    {
        while (touch.phase == TouchPhase.Began)
            if (timeInInventory >= timeLimit) // Check if the time limit has been reached
            {
                Debug.Log(timeInInventory);

                timeInInventory += Time.deltaTime; // Increment the timer by the time since the last frame
                if (fingerDown == true)
                {
                    // Execute some code here
                    Debug.Log("Time limit reached!");
                    timeInInventory = 0f; // Reset the timer
                    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(hit.transform.position).z);
                    Vector3 worldPosition = cameraMain.ScreenToWorldPoint(position);
                    GlobalSlotManager.instance.GetFromInventory(hit.collider.GetComponent<SlotManager>(), worldPosition);
                    touch.phase = TouchPhase.Ended;
                    fingerDown = false;
                    return;
                }
                else
                {
                    return;
                }
            }

    }

    void DisableInfoBox()
    {
        infoHover.SetActive(false);
    }
    void DisableCountryBox()
    {
        infoCountries.SetActive(false);
    }
    private Vector3 GetMousePos()
    {
        return cameraMain.WorldToScreenPoint(transform.position);
    }
    private RaycastHit[] CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraMain.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraMain.nearClipPlane);
        Vector3 worldMousePosFar = cameraMain.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = cameraMain.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit[] hits = Physics.RaycastAll(worldMousePosNear, worldMousePosFar - worldMousePosNear);

        return hits;
    }
}
