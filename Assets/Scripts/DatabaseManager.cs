using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public Entry db;
    private List<RectTransform> entries;
    public Transform content;
    public ScrollRect scrollRect;

    public GameObject Photo;
    public GameObject Line;
    public GameObject Quote;
    public GameObject Description;
    public GameObject Comment;
    public Button Youtube;
    public Button Link;
    public Button Book;

    private string entry;
    private string TextComment;
    private TextMeshProUGUI textToEdit;
    private bool Act;

    void Start()
    {
        entry = Static.entry;
        TextAsset asset = Resources.Load("Data/database") as TextAsset;
        db = JsonUtility.FromJson<Entry>(asset.text);
        EntryBuild();
    }

    void ShowEntry(string Text)
    {
        GameObject line = Instantiate(Line, Vector3.zero, Quaternion.identity) as GameObject;
        line.transform.SetParent(content.transform, false);
        textToEdit = line.GetComponentInChildren<TextMeshProUGUI>();
        textToEdit.text = Text;
    }

    void ShowQuote(string Text)
    {
        textToEdit = Quote.GetComponentInChildren<TextMeshProUGUI>();
        textToEdit.text = Text;
    }

    void ShowText(string text)
    {
        textToEdit = Description.GetComponentInChildren<TextMeshProUGUI>();
        textToEdit.text = text;
        RectTransform content = Description.GetComponent<RectTransform>();
        content.anchoredPosition = new Vector2(0, 0);
    }
    void ShowImage(string img)
    {
        Photo.GetComponent<Image>().sprite = Resources.Load<Sprite>(img);
    }
    public void OpenBook(string link)
    {
        Application.OpenURL(link);
    }

    public void OpenVideo(string link)
    {
        Application.OpenURL(link);
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

    public void ContinueAct()
    {
        SceneManager.LoadScene("Game");
    }

    void EntryBuild()
    {
        foreach (Database p in db.data)
        {
            if (p.name == entry)
            {
                ShowQuote(p.quote);
                ShowEntry("Name: " + p.name);
                ShowEntry("Real Name: " + p.fullName);
                ShowEntry("Country: " + p.country);
                ShowImage("Design/Portraits/" + p.img);
                Book.onClick.AddListener(() => OpenBook(p.book));
                Link.onClick.AddListener(() => OpenLink(p.link));
                Youtube.onClick.AddListener(() => OpenVideo(p.video));
                ShowText(p.text);
                TextComment = p.comment;
            }
        }
    }

    public void OpenMess()
    {
        if (Act == false && scrollRect.verticalNormalizedPosition <= 0.05f)
        {
            Act = true;
            StartCoroutine(ShowComment());
        }
    }

    IEnumerator ShowComment()
    {
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("AudioClips/click");
        yield return new WaitForSeconds(0.5f);
        player.PlayOneShot(click);
        textToEdit = Comment.GetComponentInChildren<TextMeshProUGUI>();
        textToEdit.text = TextComment;
        Comment.SetActive(true);
        scrollRect.vertical = false;
    }

    public void CloseComment()
    {
        Comment.SetActive(false);
        scrollRect.vertical = true;
    }

    public void NextScene()
    {
        if (Static.fromMenu == true) SceneManager.LoadScene("MainMenu");
        else { 
        if (Static.size > Static.sizeMax) SceneManager.LoadScene("MainMenu");
        else SceneManager.LoadScene("Messenger");
        }
    }
}
