using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private bool IsOn;
    private AudioSource player;
    public Button SoundButton;

    void Start()
    {
        IsOn = Static.sound;
        player = GetComponent<AudioSource>();
        SoundButton.onClick.AddListener(SoundButtonSwap);
        SoundButtonCheck();
    }

    void SoundButtonCheck()
    {
        if (IsOn == true)
        {
            SoundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_soundOn");
            player.mute = false;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_soundOff");
            player.mute = true;
        }
    }

    public void SoundButtonSwap()
    {
        if (IsOn == true)
        {
            SoundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_soundOff");
            player.mute = !player.mute;
            IsOn = !IsOn;
            Static.sound = IsOn;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_soundOn");
            player.mute = !player.mute;
            IsOn = !IsOn;
            Static.sound = IsOn;
        }
    }
}
