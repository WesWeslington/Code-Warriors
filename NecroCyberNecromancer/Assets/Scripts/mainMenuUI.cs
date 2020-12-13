using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject mainMenuPanel;



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("MainWorld");

    }
    public void onHelpButtonClick()
    {
        mainMenuPanel.SetActive(false);
        helpPanel.SetActive(true);
    }
    public void onCreditsButtonClick()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void onBackButtonClick()
    {
        creditsPanel.SetActive(false);
        helpPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    public void onQuitButtonClick()
    {
        Application.Quit();
    }
 
}
