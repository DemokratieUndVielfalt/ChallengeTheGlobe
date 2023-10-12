using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnValidateChangeMat : MonoBehaviour
{
    [SerializeField] Material material;
    private void OnValidate()
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material = material;
    }
}
