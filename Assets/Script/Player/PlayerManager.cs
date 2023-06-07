using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public GameObject tutorialPanel;

    public static Vector2 lastCheckPointPos = new Vector2(-6, 1);
    public CinemachineVirtualCamera VCam;

    public static int numberOfCoins;

    public TextMeshProUGUI coinsText;

    public GameObject[] playerPrefabs;
    int characterIndex;

    private bool isTutorialDisplayed;
    private bool hasDisplayedTutorial;

    private void Awake()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        VCam.m_Follow = player.transform;
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;

        // Kiểm tra xem panel hướng dẫn đã được hiển thị hay chưa
        isTutorialDisplayed = false;
        hasDisplayedTutorial = PlayerPrefs.GetInt("HasDisplayedTutorial", 0) == 1;

        // Kiểm tra xem người chơi đang ở level 1
        bool isLevel1 = SceneManager.GetActiveScene().name == "Level1";

        if (!hasDisplayedTutorial && isLevel1)
        {
            isTutorialDisplayed = true;
            tutorialPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        coinsText.text = "X " + numberOfCoins.ToString();

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }

        if (isTutorialDisplayed && Input.anyKeyDown)
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1;
            isTutorialDisplayed = false;
            PlayerPrefs.SetInt("HasDisplayedTutorial", 1);
        }
    }

    public void ReplayLevel()
    {
        PlayerPrefs.SetInt("NumberOfCoins", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }

    public void GoToMenu()
    {
        PlayerPrefs.SetInt("HasDisplayedTutorial", 0); // Đặt lại trạng thái đã hiển thị hướng dẫn khi quay lại menu
        PlayerPrefs.SetInt("NumberOfCoins", 0);

        SceneManager.LoadScene("Menu");
    }
}
