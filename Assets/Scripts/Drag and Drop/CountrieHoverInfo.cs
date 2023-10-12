using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountrieHoverInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI input;
    public void WriteCountries(string countries)
    {
        input.text = countries;
    }
    private void Start()
    {
        gameObject.SetActive(false);
        transform.localPosition = new Vector3(0,0,0);
    }
}
