using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [HideInInspector]
    public bool IsReplaying = false;
    [HideInInspector]
    public bool IsRecording = false;
    [HideInInspector]
    public bool IsBallRolling = false;

    [SerializeField]
    private AudioMixer mixer;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    //UI functions

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
    }
}

[System.Serializable]
public class GameResult
{
    public int Score;
    public int ShotsMade;
    public float TimeSpent;
}