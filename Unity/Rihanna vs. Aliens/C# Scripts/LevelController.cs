using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] GameObject pauseLabel;
    [SerializeField] float waitToLoad;
    [SerializeField] AudioClip winSFX;
    [SerializeField] AudioClip loseSFX;
    int numberOfAttcakers = 0;
    bool levelTimerFinished = false;

    bool isPaused = false;
    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
        pauseLabel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }


    public void AttackerSpawned()
    {
        numberOfAttcakers ++;
    }

    public void AttackerKilled()
    {
        numberOfAttcakers--;
        if(numberOfAttcakers <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }


    IEnumerator HandleWinCondition()
    {
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
        winLabel.SetActive(true);
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void HandleLoseCondition()
    {
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    void Pause()
    {
        pauseLabel.SetActive(true);
        Time.timeScale = 0;
    }

    void Resume()
    {
        pauseLabel.SetActive(false);
        Time.timeScale = 1;
    }


    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
