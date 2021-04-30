using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] int timeToWait = 4;
    int currentScreenIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentScreenIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentScreenIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        currentScreenIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScreenIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }


    public void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScreenIndex + 1);
    }

    public void LoadYouLose()
    {
        SceneManager.LoadScene("Lose Screen");
    }

    public void LoadOptionsScreen()
    {
        SceneManager.LoadScene("Options Screen");
    }

        public void QuitGame()
    {
        Application.Quit();
    }

}
