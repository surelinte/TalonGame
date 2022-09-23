using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject CommentData;
    public GameObject AboutWindow;
    public GameObject CreditsWindow;
    public GameObject DatabaseWindow;
    public RectTransform content;
    public Button entryButton;
    public GameObject entryButtonInactive;
    public Button backButton;
    public Button skullButton;
    private TextMeshProUGUI textToEdit;
    private Entry db;
    private bool IsMenu = true;

    void Start()
    {
        if (Static.size > Static.sizeMax)
        {
            Static.size = 0;
        }
        BackButtonCheck();
    }

    public void StartGame() // Loading data from Static
    {
        if (Static.size == 0)
        {
            Static.entrySize = 0;
        }
        Static.sceneName = "Messenger";
        SceneManager.LoadScene("Loading");
    }

    public void LoadDatabase() //Instantiate Database window 
    {
        CloseWindows();
        GameObject menu = GameObject.Find("Root/Main/Top/Menu");
        menu.SetActive(false);
        GameObject text = GameObject.Find("Root/Main/Top/Text");
        text.SetActive(false);
        TextAsset asset = Resources.Load("Data/database") as TextAsset;
        db = JsonUtility.FromJson<Entry>(asset.text);
        DatabaseWindow.SetActive(true);
        ListBuild();
        IsMenu = false;
        skullButton.interactable = false;
        skullButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_skullInactive");
        BackButtonCheck();
    }

	void ListBuild()
	{
		foreach (Database p in db.data)
		{
			if (p.number <= Static.entrySize)
			{
				Button entry = Instantiate(entryButton, Vector3.zero, Quaternion.identity) as Button;
				entry.transform.SetParent(content.transform, false);
				textToEdit = entry.GetComponentInChildren<TextMeshProUGUI>();
				textToEdit.text = p.name;
                entry.onClick.AddListener(() => OpenEntry(p.name));

            }
            else
            {
                GameObject entry = Instantiate(entryButtonInactive, Vector3.zero, Quaternion.identity) as GameObject;
                entry.transform.SetParent(content.transform, false);
                textToEdit = entry.GetComponentInChildren<TextMeshProUGUI>();
                textToEdit.text = p.name;
            }
		}
        content.anchoredPosition = new Vector2(0, 0);
        if (Static.entrySize == 0)
        {
           CommentShow();
        }

    }

    public void OpenEntry(string name)
    {
        Static.entry = name;
        Static.fromMenu = true;
        Static.sceneName = "DataBase";
        SceneManager.LoadScene("Loading");
    }

    void CommentShow()
    {
        CommentData.SetActive(true);
    }

        public void OpenAbout()
    {
        AboutWindow.SetActive(true);
        GameObject menu = GameObject.Find("Root/Main/Top/Menu");
        menu.SetActive(false);
        IsMenu = false;
        BackButtonCheck();
    }

    public void OpenCredits()
    {
        CreditsWindow.SetActive(true);
        GameObject menu = GameObject.Find("Root/Main/Top/Menu");
        menu.SetActive(false);
        IsMenu = false;
        BackButtonCheck();
    }

    public void CloseWindows() //For Back button
    {
        GameObject[] windows = GameObject.FindGameObjectsWithTag("Window");
        foreach (GameObject obj in windows)
        {
            obj.SetActive(false);
        }
        foreach (RectTransform entry in content)
        {
            Destroy(entry.gameObject);
        }
        CommentData.SetActive(false);
        GameObject text = GameObject.Find("Root/Main/Top/Text");
        text.SetActive(true);
        GameObject menu = GameObject.Find("Root/Main/Top/Menu");
        menu.SetActive(true);
        IsMenu = true;
        skullButton.interactable = true;
        skullButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_skull");
        BackButtonCheck();
    }

    void BackButtonCheck()
    {
        backButton.onClick.RemoveListener(Exit);
        if (IsMenu == false)
        {
            backButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_back");
            backButton.onClick.AddListener(CloseWindows);
        }
        else
        {
            backButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Buttons/roundButton_exit");
            backButton.onClick.AddListener(Exit);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("exit");
        /*if (IsMenu == true)
        {
            Application.Quit();
            Debug.Log("exit");
        }
        else CloseWindows();*/
    }
}
