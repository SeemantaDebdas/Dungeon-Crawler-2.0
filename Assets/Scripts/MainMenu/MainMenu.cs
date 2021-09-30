using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioClip ButtonSelection;
    public void StartButton()
    {
        AudioSource.PlayClipAtPoint(ButtonSelection, Camera.main.transform.position);
        int nextBuildIdx = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextBuildIdx);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
