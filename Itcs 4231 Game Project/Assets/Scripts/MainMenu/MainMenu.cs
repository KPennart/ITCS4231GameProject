using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;
    private AudioSource audio;
    [SerializeField] private AudioClip music;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = music;
        audio.loop = true;
        audio.Play();
    }

    private void OnMouseUp()
    {
        if (isStart) {

            audio.Stop();
            SceneManager.LoadScene(1);
        }
        if (isQuit)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
