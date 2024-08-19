using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    public GameObject panelLoading;

    public string nameScene;
    public float timeDelay;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string nameScene)
    {
        this.nameScene = nameScene;
        panelLoading.SetActive(true);

        Time.timeScale = 1;

        Invoke(nameof(ChangingScene), timeDelay);
    }

    private void ChangingScene()
    {
        SceneManager.LoadScene(nameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
