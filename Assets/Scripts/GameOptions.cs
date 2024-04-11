using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameOptions : MonoBehaviour
{
    public static GameOptions instance;

    public HighScore highScore;
    public HighScore playerScore;

    private string PATH_SAVEFILE;

    [Serializable]
    public class HighScore
    {
        public string playerName;
        public int score;

        public HighScore()
        {
            playerName = "";
            score = 0;
        }
        public HighScore(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        PATH_SAVEFILE = Application.persistentDataPath + "/savefile.json";

        highScore = new HighScore();
        playerScore = new HighScore();

    }
    public void SaveHighScore(GameOptions.HighScore dataToSave)
    {
        string json = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(PATH_SAVEFILE, json);

        //Debug.Log($"Saved to {PATH_SAVEFILE}");
    }

    public GameOptions.HighScore LoadHighScore()
    {
        GameOptions.HighScore loadedData = new HighScore();

        if (File.Exists(PATH_SAVEFILE))
        {
            string json = File.ReadAllText(PATH_SAVEFILE);
            loadedData = JsonUtility.FromJson<GameOptions.HighScore>(json);
            
            //Debug.Log($"Loaded from {PATH_SAVEFILE}");
        }

        return loadedData;
    }
}
