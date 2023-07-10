using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentProgress = 0;
    public int maxProgress = 2;
    public AudioSource win;
    public AudioSource active;
    //public GameObject ui;

    void Update ()
    {

        if (currentProgress >= maxProgress)
        {
            // Level completed!
            Debug.Log("Level completed!");
            win.Play();
            StartCoroutine(golevel("Success"));
            //GoLevel("Success");

            //ui.SetActive(true);
            
        }
    }

    public void Progress()
    {
        currentProgress++;
        active.Play();
    }
    public void GoLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    IEnumerator golevel(string levelname)
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelname);


    }
}
