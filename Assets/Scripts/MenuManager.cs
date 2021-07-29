using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using TMPro;
public class MenuManager : MonoBehaviour
{
    static public MenuManager Instance;
    public string PlayerName;
    Data CurrentHighScore = new Data();
    public string HighScoreText;

    [SerializeField] TMP_InputField InputName; 
    [SerializeField] TMP_Text BestScore; 
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void StartGame()
    {
        PlayerName = InputName.text;
        SceneManager.LoadScene(1);
    }


    [Serializable]
    class Data
    {
        public string Name;
        public int Score;
    }

    public void SaveScore(int score)
    {
        if (score > CurrentHighScore.Score)
        {
            Data data = new Data();
            data.Name = PlayerName;
            data.Score = score;
            CurrentHighScore = data;
            HighScoreText = $"{CurrentHighScore.Name} : {CurrentHighScore.Score}";

            string json = JsonUtility.ToJson(data);
            string path = Application.persistentDataPath + "/savefile.json";
            File.WriteAllText(path, json);
        }
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Data data = JsonUtility.FromJson<Data>(json);

            CurrentHighScore = data;
            HighScoreText = $"{CurrentHighScore.Name} : {CurrentHighScore.Score}";
            BestScore.text = $"Best score : {HighScoreText}";
        }
    }
}
