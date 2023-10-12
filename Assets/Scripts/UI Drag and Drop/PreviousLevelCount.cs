using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousLevelCount : MonoBehaviour
{
    [SerializeField] List<GameObject> level1Bevölkerung = new List<GameObject>();
    [SerializeField] List<GameObject> level2Reichtum = new List<GameObject>();
    [SerializeField] List<GameObject> level3Geflüchete = new List<GameObject>();
    [SerializeField] List<GameObject> level4Co2 = new List<GameObject>();

    public int level1Count;
    public int level2Count;
    public int level3Count;
    public int level4Count;

    float delay = 0.36f;

    public int rightNumberForMe;

    [SerializeField] GameObject rightAnswers;

    public void LevelUpdate(int level1, int level2, int level3, int level4, int currlevel)
    {
        StartCoroutine(LevelUpdateWithDelay(level1, level2, level3, level4, currlevel));
    }
    IEnumerator LevelUpdateWithDelay(int level1, int level2, int level3, int level4, int currlevel)
    {
        for (int a = 0; a < level1Bevölkerung.Count; a++)
        {
            rightNumberForMe = level1;

            level1Bevölkerung[a].SetActive(false);
            level2Reichtum[a].SetActive(false);
            level3Geflüchete[a].SetActive(false);
            level4Co2[a].SetActive(false);

        }
        yield return new WaitForSecondsRealtime(2f);
        if (currlevel > 1)
        {
            rightNumberForMe = level2;
            for (int a = 0; a < level1Bevölkerung.Count; a++)
            {
                if (a <= level1 - 1)
                {
                    level1Count++;
                    SoundManager.instance.PlayStackngSound();
                    level1Bevölkerung[a].SetActive(true);
                    yield return new WaitForSecondsRealtime(delay);

                }

            }
            if (currlevel > 2)
            {
                rightNumberForMe = level3;

                for (int a = 0; a < level2Reichtum.Count; a++)
                {
                    if (a <= level2 - 1)
                    {
                        level2Count++;
                        SoundManager.instance.PlayStackngSound();

                        level2Reichtum[a].SetActive(true);
                        yield return new WaitForSecondsRealtime(delay);

                    }

                }
            }
            if (currlevel > 3)
            {
                rightNumberForMe = level4;

                for (int a = 0; a < level2Reichtum.Count; a++)
                {
                    if (a <= level3 - 1)
                    {
                        level3Count++;
                        SoundManager.instance.PlayStackngSound();

                        level3Geflüchete[a].SetActive(true);
                        yield return new WaitForSecondsRealtime(delay);

                    }

                }
            }
            else if (currlevel == 4)
            {
                rightNumberForMe = level4;
            }
            if (currlevel == 5)
            {
                rightNumberForMe = level4;
                for (int a = 0; a < level2Reichtum.Count; a++)
                {
                    if (a <= level4 - 1)
                    {
                        level4Count++;
                        SoundManager.instance.PlayStackngSound();

                        level4Co2[a].SetActive(true);
                        yield return new WaitForSecondsRealtime(delay);

                    }

                }
            }

        }
    }
    public void CorrektAnswer()
    {
        StartCoroutine(CorrektAnswerDelay());
    }
    IEnumerator CorrektAnswerDelay()
    {
        yield return new WaitForSecondsRealtime(2.7f);

        for (int i = 0; i < rightAnswers.transform.childCount; i++)
        {
            if (rightNumberForMe > i)
            {
                yield return new WaitForSecondsRealtime(.5f);

                SoundManager.instance.PlaySlotIn(i);
                GameObject currChild = rightAnswers.transform.GetChild(i).gameObject;
                GameObject currChildOfChild = currChild.transform.GetChild(0).gameObject;
                currChild.SetActive(true);
                currChildOfChild.transform.localScale = Vector3.one / 2;

                LeanTween.scale(currChildOfChild, Vector3.one, .3f).setEase(LeanTweenType.easeOutBack);

            }
        }
        GetComponentInParent<SlotManager>().currSlot = rightNumberForMe;
        GetComponentInParent<Continents>().UpdateText();
    }


}
