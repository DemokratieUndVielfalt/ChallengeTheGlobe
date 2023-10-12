using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectionMneu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnCLickDragandDrop()
    {
        SceneManager.LoadScene(1);
    }
    public void OnCLickKreisdiagramm()
    {
        SceneManager.LoadScene(2);

    }
    public void OnCLickWürfel()
    {
        SceneManager.LoadScene(3);

    }

}
