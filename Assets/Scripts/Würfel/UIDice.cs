using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDice : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tips;
    [SerializeField] TextMeshProUGUI levelTheme;
    [SerializeField] string canSelect;
    [SerializeField] string canThrow;
    [SerializeField] string levelThemeContent;
    private void Start()
    {
        UpdateTipsSelect();
        TextwithTheme();
    }
    public void UpdateTipsSelect()
    {
        tips.text = canSelect;
    }
    public void UpdateTipsThrow()
    {
        tips.text = canThrow;

    }
    public void UpdateTipsEmpty()
    {
        tips.text = "";

    }
    public void OnClickRestart()
    {
        InputManagerDice.instance.Restart();
    }
    void TextwithTheme()
    {
        levelTheme.text = levelThemeContent;
    }

}
