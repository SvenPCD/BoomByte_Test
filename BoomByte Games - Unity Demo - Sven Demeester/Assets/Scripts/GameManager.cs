using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager Instance { get { return _Instance; } }

    [HideInInspector]
    public bool IsReplaying = false;
    [HideInInspector]
    public bool IsRecording = false;
    [HideInInspector]
    public bool IsBallRolling = false;

    [SerializeField]
    private AudioMixer _Mixer;

    [HideInInspector]
    public string SaveFile;

    private void Awake()
    {

        SaveFile = Application.persistentDataPath + "/SaveFileBilliard.json";

        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        IsReplaying = false;
        IsRecording = false;
        IsBallRolling = false;

        _Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //UI functions

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetVolume(float sliderValue)
    {
        _Mixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
    }
}

[System.Serializable]
public class GameResult
{
    public int Score;
    public int ShotsMade;
    public int TimeSpent;
}