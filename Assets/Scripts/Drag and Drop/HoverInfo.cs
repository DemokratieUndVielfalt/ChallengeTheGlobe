using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI continentName;
    [SerializeField] TextMeshProUGUI level1Text;
    [SerializeField] TextMeshProUGUI level2Text;
    [SerializeField] TextMeshProUGUI level3Text;
    [SerializeField] TextMeshProUGUI level4Text;

    [SerializeField] GameObject level5Images;
    private void Start()
    {
       

        //gameObject.transform.position = new Vector3(-1000, 0, 0);
    }
    public void UpdateText(string continent, float level1, float level2, float level3,float level4)
    {
        continentName.text = continent;
        if (level1 == 0)
        {
            level1Text.text = "";
        }
        else
        {
            level1Text.text = level1 / 0.2 + "%";
        }
        if (level2 == 0)
        {
            level2Text.text = "";
        }
        else
        {
            level2Text.text = level2 / 0.2 + "%";
        }

        if (level3 == 0)
        {
            level3Text.text = "";
        }
        else
        {
            level3Text.text = level3 / 0.2 + "%";
        }
        if (level4 == 0)
        {
            //level3Text.text = "";
        }
        else
        {
            level5Images.SetActive(true);

            level4Text.text = level4 / 0.2 + "%";
        }


    }
}
