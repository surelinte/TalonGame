using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour
{
    public void TaskOnDown()
    {
        transform.localScale = new Vector2(0.97f, 0.97f);
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("AudioClips/click");
        player.PlayOneShot(click);

    }

    public void TaskOnUp()
    {
        transform.localScale = new Vector2(1, 1);
    }
}
