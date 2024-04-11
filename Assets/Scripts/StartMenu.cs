#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TMP_InputField playerNameText;

    void Start()
    {
        playerNameText.text = "";

        // Load high score
        GameOptions.instance.highScore = GameOptions.instance.LoadHighScore();

        // if high score loaded, update highScore
        if (GameOptions.instance.highScore.score > 0)
        {
            highScoreText.text = $"High Score : {GameOptions.instance.highScore.playerName} : {GameOptions.instance.highScore.score}";

            // Set only the player name (not the score) to the high score name, for pre-completeion editing purposes on the text entry
            GameOptions.instance.playerScore.playerName = GameOptions.instance.highScore.playerName;
        }
        else
            highScoreText.text = "High Score : Name : 0";

        playerNameText.text = GameOptions.instance.playerScore.playerName;

    }

    public void PlayerNameText_Deselected()
    {
        if (playerNameText.text.Trim().Length == 0)
            playerNameText.text = "";
        else
        {
            playerNameText.text = playerNameText.text.Trim();
        }

        GameOptions.instance.playerScore.playerName = playerNameText.text;

    }

    public void GameStart()
    {
        // Check for a valid player's name
        if (GameOptions.instance.playerScore.playerName.Length > 0)
        {
            // Load Game Main Scene
            SceneManager.LoadScene("main");
        }
    }
    public void GameQuit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

}
