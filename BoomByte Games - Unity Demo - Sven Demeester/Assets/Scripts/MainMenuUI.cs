using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{

    string saveFile;
    [SerializeField]
    private Text _TextLastGame;
    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/SaveFileBilliard.json";

        if (File.Exists(saveFile))
        {
            string Contents = File.ReadAllText(saveFile);

            GameResult gameResult = JsonUtility.FromJson<GameResult>(Contents);

            _TextLastGame.text = "Score: " + gameResult.Score.ToString() + "\n" + "Shots Made: " + gameResult.ShotsMade.ToString() + "\n" + "timespent: " + gameResult.TimeSpent.ToString() ;
            

        }
        else
        {
            _TextLastGame.text = "No game recorded yet";
            WriteFile();
        }
    }

    public void WriteFile()
    {

        GameResult Test = new GameResult();

        Test.Score = 2;
        Test.ShotsMade = 10;
        Test.TimeSpent = 2f;

        string jsonString = JsonUtility.ToJson(Test);

        File.WriteAllText(saveFile, jsonString);
    }


    public void StartGame() { StartCoroutine(Startgame()); }

    IEnumerator Startgame()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.StartGame();
    }

    public void SetVolume( float sliderValue){GameManager.Instance.SetVolume(sliderValue);}
}


// score
// shots made
// Time spent