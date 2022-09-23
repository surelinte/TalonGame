using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MessengerManager : MonoBehaviour
{
	public ScrollRect window;
	public RectTransform input; // input message prefab
	public RectTransform output; // output message prefab
	public RectTransform typingIn; // typing input message prefab
	public int offset = 20; // spacing between messages
	

	private Vector2 delta;
	private Vector2 e_Pos;
	private List<RectTransform> messages;

	private int size;
	private string entry;

	private AudioSource player;
	private float curY;
	private int id;
	private int counter = 0;
	private string text;
	private string NewText;
	private int OutLen;
	private TextMeshProUGUI textToEdit;
	private TextMeshProUGUI Dots;
	public Button Continue;
	public Button SendAnswer;
	public Data db;


	void Start()
	{
		player = (AudioSource)FindObjectOfType(typeof(AudioSource));
		//список сообщений и положение объектов
		messages = new List<RectTransform>();
		delta = input.sizeDelta;
		delta.y += offset;
		e_Pos = new Vector2(0, -delta.y / 2);
		//загрузка данных из джейсон
		TextAsset asset = Resources.Load("Data/messages") as TextAsset;
		db = JsonUtility.FromJson<Data>(asset.text);
		//загрузка данных из статик
		entry = Static.entry;
		size = Static.size;
		//если старт игры - то +1 для запуска скрипта, потом пусть это будет на катсцене
		//если дошло до конца списка сообщений, то игра идет сначала
		if (size == 0) size++;
		DialogueLogic(); //start
	}

	public void DialogueLogic()
	{
		foreach (Text p in db.text)
		{
			if (p.id == size)
			{
				if (p.messageField == 1) //input message
				{
					StartCoroutine(Typing(p.length, p.messageText));
				}
				if (p.messageField == 2) //output message
				{
					StartCoroutine(Answering(p.length, p.messageText));
				}
				if (p.messageField == 3) //end of dialogue
				{
					StartCoroutine(Continuing(p.messageText));
					break;
				}
			}

		}
	}
	IEnumerator Typing(int len, string text)
	{
		counter++;
		yield return new WaitForSeconds(0.5f);
		AudioClip click = Resources.Load<AudioClip>("AudioClips/typing");
		player.PlayOneShot(click);
		AddToList();
		RectTransform typ = Instantiate(typingIn) as RectTransform;
		typ.anchoredPosition = new Vector2(0, e_Pos.y - curY);
		Animator mesIn = typingIn.GetComponent<Animator>();
		typ.gameObject.SetActive(true);
		typ.SetParent(window.content);
		typ.localScale = Vector3.one;
		typ.anchoredPosition = new Vector2(e_Pos.x, e_Pos.y - curY);
		Dots = typ.GetComponentInChildren<TextMeshProUGUI>();
		Dots.text = ".";
		messages.Add(typ);
		yield return new WaitForSeconds(0.3f);
		Dots.text = "..";
		yield return new WaitForSeconds(0.3f);
		Dots.text = "...";
		yield return new WaitForSeconds(0.3f);
		Destroy(typ.gameObject);
		messages.Remove(typ);
	BuildInput(len, text);
	}

	public void AddToList()
	{
		curY = 0;
		RectContent();
		foreach (RectTransform b in messages)
		{
			b.anchoredPosition = new Vector2(e_Pos.x, e_Pos.y - curY);
			curY += delta.y;
		}
	}

	void RectContent()
	{
		float height = delta.y * counter;
		window.content.sizeDelta = new Vector2(window.content.sizeDelta.x, height);
		window.content.anchoredPosition = new Vector2(0, height);
	}

	void BuildInput(int len, string text) // создание нового элемента INPUT и настройка параметров
	{
		AddToList();
		AudioClip click = Resources.Load<AudioClip>("AudioClips/input");
		player.PlayOneShot(click);
		RectTransform clone = Instantiate(input) as RectTransform;
		clone.SetParent(window.content);
		clone.localScale = Vector3.one;
		clone.anchoredPosition = new Vector2(0, e_Pos.y - curY);
		clone.sizeDelta = new Vector2(len * 30, clone.sizeDelta.y);
		textToEdit = clone.GetComponentInChildren<TextMeshProUGUI>();
		textToEdit.text = text;
		text = NewText;
		Animator anim = input.GetComponent<Animator>();
		messages.Add(clone);
		size++;
		DialogueLogic();
	}

	IEnumerator Answering(int len, string text)
    {
		yield return new WaitForSeconds(1);
		textToEdit = SendAnswer.GetComponentInChildren<TextMeshProUGUI>();
		SendAnswer.gameObject.SetActive(true);
		AudioClip click = Resources.Load<AudioClip>("AudioClips/click");
		player.PlayOneShot(click);
		textToEdit.text = text;
		NewText = text;
		OutLen = len;
	}


	public void BuildOutput()
	{
		AudioClip click = Resources.Load<AudioClip>("AudioClips/output");
		player.PlayOneShot(click);
		SendAnswer.gameObject.SetActive(false);
		AddToList();
		RectTransform clone = Instantiate(output) as RectTransform;
		clone.SetParent(window.content);
		clone.localScale = Vector3.one;
		clone.anchoredPosition = new Vector2(0, e_Pos.y - curY);
		clone.sizeDelta = new Vector2(OutLen * 30, clone.sizeDelta.y);
		Animator anim = output.GetComponent<Animator>();
		textToEdit = clone.GetComponentInChildren<TextMeshProUGUI>();
		textToEdit.text = NewText;
		messages.Add(clone);
		size++;
		Static.size = size;
		counter++;
		DialogueLogic();
	}

	IEnumerator Continuing(string text)
    {
		yield return new WaitForSeconds(0.5f);
		AudioClip click = Resources.Load<AudioClip>("AudioClips/loading");
		player.PlayOneShot(click);
		size++;
		Static.entrySize++;
		entry = text;
		Continue.gameObject.SetActive(true);
		Continue.onClick.AddListener(() => NextScene());
	}

	public void NextScene()
	{
		Continue.gameObject.SetActive(false);
		foreach (RectTransform b in messages)
		{
			Destroy(b.gameObject);
		}
		messages = new List<RectTransform>();
		RectContent();
		//save the data
		Static.entry = entry;
		Static.size = size;
		Static.fromMenu = false;
		Static.sceneName = "DataBase";
		SceneManager.LoadScene("Loading");
	}
}
