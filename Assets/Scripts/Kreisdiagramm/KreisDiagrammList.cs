using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KreisDiagrammList : MonoBehaviour
{
    public List<bool> columnFree = new List<bool>();
    public List<KreisdiagrammManager> KDList = new List<KreisdiagrammManager>();
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 16; i++)
        {
            columnFree.Add(false);
        }
    }

    // Update is called once per frame
    public void UpdateKDManagers(int activeCollumn, int exception, Color newColor)
    {
        int count = 0;
        foreach (KreisdiagrammManager kd in KDList)
        {
            if (count != exception)
            {
                Debug.Log("1");
                kd.UpdateColumn(activeCollumn, newColor);

            }
            count++;
        }
    }
}
