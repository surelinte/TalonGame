using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollFade : MonoBehaviour
{
    private ScrollRect scrollRect;
    private GameObject footer;

    void Start()
    {
        scrollRect = this.GetComponent<ScrollRect>();
        footer = GameObject.Find("BottomFade");
    }
    
    public void FadeManager()
    {
        if (scrollRect.verticalNormalizedPosition <= 0.08f) footer.SetActive(false);
        else footer.SetActive(true);
    }
}
