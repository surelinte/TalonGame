using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeButtonManager : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
