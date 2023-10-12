using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageBox : MonoBehaviour
{
    public RawImage image;
    Vector3 ogScale;

    // Start is called before the first frame update

    public void updateImage(GameObject parent, Texture texture)
    {

        gameObject.transform.SetParent(parent.transform);
        ogScale = transform.localScale;
        transform.localScale = Vector3.zero;
       // Debug.Log(ogScale);
        image.texture = texture;
        LeanTween.scale(gameObject, ogScale, 0.3f).setEase(LeanTweenType.easeOutBack);


    }
}
