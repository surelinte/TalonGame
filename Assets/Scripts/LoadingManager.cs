using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    private AsyncOperation async;
    public GameObject Loading;
    public GameObject Access;
    public GameObject Icon;
    public GameObject Text;

    void Update()
    {
        if (Input.GetMouseButton(0)) async.allowSceneActivation = true;
    }

	IEnumerator Start()
	{
        //set start objects
        Loading.SetActive(true);
        Access.SetActive(false);
        Text.SetActive(true);
        TextMeshProUGUI textToEdit = Text.GetComponentInChildren<TextMeshProUGUI>();
        textToEdit.text = "please, wait";
        //generate pseudo-loading animation
        System.Random random = new System.Random();
        int number = random.Next(0, 3);
        Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Elements/loading/loading" + number);
        yield return new WaitForSeconds(0.3f);
        number = random.Next(4, 9);
        Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Elements/loading/loading" + number);
        yield return new WaitForSeconds(0.3f);
        number = random.Next(10, 18);
        Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Elements/loading/loading" + number);
        yield return new WaitForSeconds(0.3f);
        //play sound
        AudioSource player = (AudioSource)FindObjectOfType(typeof(AudioSource));
        AudioClip click = Resources.Load<AudioClip>("AudioClips/loading");
        player.PlayOneShot(click);
        //set final objects
        Loading.SetActive(false);
        Access.SetActive(true);
        Icon.GetComponent<Image>().sprite = Resources.Load<Sprite>("Design/Elements/loading/loading19");
        Text.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        textToEdit.text = "tap to continue";
       Text.SetActive(true);
        async = SceneManager.LoadSceneAsync(Static.sceneName); //get name of the next scene
        //load the next scene
        yield return true;
		async.allowSceneActivation = false;
	}

}
