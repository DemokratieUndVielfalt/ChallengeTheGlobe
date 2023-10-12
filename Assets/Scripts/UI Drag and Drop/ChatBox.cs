using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ChatBox : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI chatText;
    public string chatContent;
    [SerializeField] GameObject bg;
    RawImage bgColor;
    Vector3 ogScale;
    [SerializeField] RawImage ownIcon;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] List<Texture> icons = new List<Texture>();
    [SerializeField] List<Color> bgColors = new List<Color>();

    public RawImage image;
    Vector3 ogScaleImage;

    //private RectTransform _rectTransform, _parentRectTransform;
    //private VerticalLayoutGroup verticalLayoutGroup;

    //void OnEnable()
    //{
    //    UpdateWidth();
    //}

    //void OnRectTransformDimensionsChange()
    //{
    //    UpdateWidth();
    //}


    //private void UpdateWidth()
    //{
    //    if (verticalLayoutGroup == null || _rectTransform == null || _parentRectTransform == null)
    //    {
    //        verticalLayoutGroup = GetComponentInParent<VerticalLayoutGroup>();
    //        if (verticalLayoutGroup != null)
    //        {
    //            _parentRectTransform = verticalLayoutGroup.GetComponent<RectTransform>();
    //            _rectTransform = GetComponent<RectTransform>();
    //            _rectTransform.pivot = new Vector2(0, 1);
    //            _rectTransform.sizeDelta = new Vector2(_parentRectTransform.rect.size.x - (verticalLayoutGroup.padding.left + verticalLayoutGroup.padding.right), _rectTransform.sizeDelta.y);
    //        }
    //    }
    //    else
    //    {
    //        _rectTransform.sizeDelta = new Vector2(_parentRectTransform.rect.size.x - (verticalLayoutGroup.padding.left + verticalLayoutGroup.padding.right), _rectTransform.sizeDelta.y);
    //    }
    //}

    private void Start()
    {
    }
    public void updateContent(string content, GameObject parent)
    {
        chatContent = chatText.text;
        gameObject.transform.SetParent(parent.transform);

        chatText.text = content;
        bgColor = bg.GetComponent<RawImage>();
        ogScale = transform.localScale;
        transform.localScale = Vector3.zero;


        LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);

    }
    public void UpdateParent(GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);

    }
    public void UpdateNPC(int nr)
    {
        bgColor = bg.GetComponent<RawImage>();

        if (nr == 0)
        {
            npcName.text = "Leila";
            ownIcon.texture = icons[0];
            bgColor.color = bgColors[0];
        }
        else if (nr == 1)
        {
            ownIcon.texture = icons[1];
            bgColor.color = bgColors[1];
            npcName.text = "Filip";

        }
        else if (nr == 2)
        {
            ownIcon.texture = icons[2];
            bgColor.color = bgColors[2];
            npcName.text = "Mai";

        }
        else
        {
            ownIcon.texture = icons[3];
            bgColor.color = bgColors[3];
            npcName.text = "Kendy";

        }
    }
    public void updateImage(GameObject parent, Texture texture)
    {

        gameObject.transform.SetParent(parent.transform);
        ogScaleImage = transform.localScale;
        transform.localScale = Vector3.zero;
        image.texture = texture;
        LeanTween.scale(gameObject, Vector3.one, 0.3f).setEase(LeanTweenType.easeOutBack);
    }


   
        //[SerializeField] private RectTransform mainCanvas;
        //private RectTransform rectTransform;

        //private void OnEnable()
        //{
        //    UpdateRect();
        //}

        //private void OnRectTransformDimensionsChange()
        //{
        //    UpdateRect();
        //}

        //private void UpdateRect()
        //{
        //    Debug.Log("changing");
        //    rectTransform = GetComponent<RectTransform>();
        //    rectTransform.sizeDelta = new Vector2(mainCanvas.sizeDelta.x, mainCanvas.sizeDelta.y);
        //}
    
}

