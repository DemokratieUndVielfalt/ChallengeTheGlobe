using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] AudioSource button;
    SoundList buttonList;
    [SerializeField] AudioSource message;
    [SerializeField] AudioSource handyDown;
    [SerializeField] AudioSource handyUp;
    [SerializeField] AudioSource handyScreen;
    [SerializeField] AudioSource infoHoverIn;
    [SerializeField] AudioSource infoHoverOut;
    [SerializeField] AudioSource blockStacking;
    SoundList blockStackingList;
    [SerializeField] AudioSource fadeToBlack;


    [SerializeField] AudioSource slotOut;
    [SerializeField] AudioSource slotIn;
    SoundList slotInList;
    [SerializeField] AudioSource slotFull;
    private void Start()
    {
        blockStackingList = blockStacking.gameObject.GetComponent<SoundList>();
        slotInList = slotIn.gameObject.GetComponent<SoundList>();
        buttonList = button.gameObject.GetComponent<SoundList>();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            instance = this;

        }
        else
        {
            instance = this;
        }
    }
    public void PlayButtonSound()
    {
        buttonList.PlayAtRandom();
    }
    public void PlayMessageSound()
    {
        message.Play();
    }
    public void PlayPhoneOnOff()
    {
        handyDown.Play();
    }
    public void PlayStackngSound()
    {
        blockStackingList.PlayAtRandom();
    }
    public void PlaySlotIn(float nr)
    {
        nr = 1 + (nr / 20);
        //Debug.Log(nr);
        slotInList.PlayAtRandom();
        slotIn.pitch = nr;

    }
    public void PlaySlotOut()
    {
        slotOut.Play();
    }
    public void PlaySlotFull()
    {
        slotFull.Play();
    }
    public void PlayFadeToBlack()
    {
        fadeToBlack.Play();
    }
    public void InfoHoverOn()
    {
        infoHoverIn.Play();
    }
    public void InfoHoverOff()
    {
        infoHoverOut.Play();
    }
    public void HandyUpSound()
    {
        handyUp.Play();
    }

}
