using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpdate : MonoBehaviour
{
    [Range(1, 5)]
    public int level;
    [SerializeField] List<PreviousLevelCount> levelCounts = new List<PreviousLevelCount>();
    public ResponsesToSolution responses;
    bool once = false;

    private void Start()
    {
        if (!once)
        {
            UpdateLateSlot();
            once = true;
        }
    }
    void UpdateLateSlot()
    {

        int a = 0;
        foreach (PreviousLevelCount lvl in levelCounts)
        {
            lvl.LevelUpdate(LevelManager.instance.previousLevelSlots[0][a],
                LevelManager.instance.previousLevelSlots[1][a],
                LevelManager.instance.previousLevelSlots[2][a],
                LevelManager.instance.previousLevelSlots[3][a],
                level);
            a++;
        }

    }
    public void UpdateRightSlots()
    {
        foreach (PreviousLevelCount lvl in levelCounts)
        {
            lvl.CorrektAnswer();
        }
    }
    public void OnClickOpenMainMenu()
    {
        SoundManager.instance.PlayFadeToBlack();
        LevelManager.instance.ActivateMainMenu();
        LeanTween.value(gameObject, 0, 1f, .5f).setOnComplete(DisableObject);

    }
    public void DisableObject()
    {
        gameObject.SetActive(false);

    }

}
