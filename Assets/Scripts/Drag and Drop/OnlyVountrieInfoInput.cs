using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyVountrieInfoInput : MonoBehaviour
{

    GameObject infoHover;
    GameObject infoCountries;
    Transform infoHoverTransform;

    private Vector3 mousePos;
    bool infoHoveractive = false;
    bool infoCountriesactive = false;


    Camera cameraMain;


    private void Start()
    {

        InputManager inputManager = GetComponent<InputManager>();
        infoHover = inputManager.infoHover;
        infoCountries = inputManager.infoCountries;
        infoHoverTransform = infoHover.GetComponent<Transform>();
        infoHover.SetActive(true);
        cameraMain = Camera.main;
       // enabled=false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = CastRay();
            foreach (RaycastHit hit in hits)
            {


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





            }
        }
        else if (Input.GetMouseButtonUp(0))
        {

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


            Cursor.visible = true;
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
