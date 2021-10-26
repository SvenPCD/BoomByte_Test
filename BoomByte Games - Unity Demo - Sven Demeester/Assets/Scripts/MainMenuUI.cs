using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Text _TextLastGame;
    private void Awake()
    {

        if (File.Exists(GameManager.Instance.SaveFile))
        {
            string Contents = File.ReadAllText(GameManager.Instance.SaveFile);

            GameResult gameResult = JsonUtility.FromJson<GameResult>(Contents);

            StringBuilder SB = new StringBuilder();
            SB.Append("Score: ");
            SB.Append(gameResult.Score.ToString());
            SB.Append("\n");
            SB.Append("ShotCount: ");
            SB.Append(gameResult.ShotsMade.ToString());
            SB.Append("\n");
            SB.Append("Time: ");
            SB.Append(gameResult.TimeSpent.ToString());
            SB.Append(" s");

            _TextLastGame.text = SB.ToString();

        }
        else
        {
            _TextLastGame.text = "No game recorded yet";
        }
    }


    public void StartGame() { StartCoroutine(Startgame()); }

    IEnumerator Startgame()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.StartGame();
    }

    public void SetVolume( float sliderValue){GameManager.Instance.SetVolume(sliderValue);}
}