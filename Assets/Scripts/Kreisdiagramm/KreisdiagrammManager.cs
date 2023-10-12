using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KreisdiagrammManager : MonoBehaviour
{
    [SerializeField] List<GameObject> columns = new List<GameObject>();
    [SerializeField] List<GameObject> activeColumns = new List<GameObject>();
    [SerializeField] Color inactiveColor;
    [SerializeField] Color activeColor;
    [SerializeField] Color assignedColor;
    int kDint;

    [SerializeField] TextMeshProUGUI count;

    KreisDiagrammList kDList;
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        kDList = GameObject.FindObjectOfType<KreisDiagrammList>();

        kDint = kDList.KDList.IndexOf(this);

        int count = 0;
        foreach (Transform child in transform)
        {

            GameObject columnsAdd = child.gameObject;
            columns.Add(columnsAdd);
            columnsAdd.gameObject.GetComponent<SpriteRenderer>().color = inactiveColor;
            count++;
        }
    }

    public void SetActive()
    {

    }
    public void SetInactive()
    {

    }

    public void InputColumn(GameObject column)
    {
        int i = columns.IndexOf(column);
        if (activeColumns.Contains(column))
        {

            column.GetComponent<SpriteRenderer>().color = inactiveColor;
            activeColumns.Remove(column);
            kDList.columnFree[i] = false;

            kDList.UpdateKDManagers(i, kDint, inactiveColor);
            UpdateText();

        }
        else
        {

            if (kDList.columnFree[i] == false)
            {

                column.GetComponent<SpriteRenderer>().color = activeColor;
                kDList.columnFree[i] = true;
                activeColumns.Add(column);
                kDList.UpdateKDManagers(i,kDint, assignedColor);
                UpdateText();

            }

        }
    }
    public void UpdateColumn(int i, Color newcolor)

    {
        columns[i].GetComponent<SpriteRenderer>().color = newcolor;
        UpdateText();
    }
    void UpdateText()
    {
        int nr = activeColumns.Count * 10;
        count.text = nr.ToString();
    }
}
