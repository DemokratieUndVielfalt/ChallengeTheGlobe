using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] GameObject[] slots;

    List<GameObject> fullslots = new List<GameObject>();
    [HideInInspector] public int currSlot = 0;
    public bool full = false;

    //  public Transform spawnPoint;

    Continents ownContinent;

    // Start is called before the first frame update
    void Start()
    {
        ownContinent = GetComponentInParent<Continents>();
        int count = 0;
        foreach (GameObject slot in slots)
        {
            GameObject slotAdd = slot.transform.GetChild(0).gameObject;
            fullslots.Add(slotAdd);
            fullslots[count].SetActive(false);
            count++;
        }

    }

    public void FillSlot()
    {
        if (currSlot <= slots.Length - 1)
        {
            SoundManager.instance.PlaySlotIn(currSlot);
            fullslots[currSlot].SetActive(true);
            fullslots[currSlot].transform.localScale = Vector3.one / 2;
            LeanTween.cancel(fullslots[currSlot]);
            LeanTween.scale(fullslots[currSlot], Vector3.one, .3f).setEase(LeanTweenType.easeOutBack);
            currSlot++;
        }
        else
        {
            full = true;
            SoundManager.instance.PlaySlotFull();

        }
        ownContinent.UpdateText();

    }
    public bool isFull()
    {
        // Debug.Log(full);
        if (currSlot <= slots.Length - 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void DeleteSlot()
    {
       // Debug.Log(currSlot);
        if (currSlot >= 0)
        {
            SoundManager.instance.PlaySlotOut();
            currSlot--;
            fullslots[currSlot].SetActive(false);
            full = false;
        }
        else
        {
            SoundManager.instance.PlaySlotFull();

        }

        ownContinent.UpdateText();
    }

}
