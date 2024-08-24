using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onPlayClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public GameObject TutorialPage;
    public GameObject MainPage;

    public void tutorialBtn()
    {
        TutorialPage.SetActive(true);
        MainPage.SetActive(false);
    }

    public void BackBtn()
    {
        MainPage.SetActive(true);
        TutorialPage.SetActive(false);
    }
}
