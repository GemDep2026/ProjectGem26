using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton; // Tambahkan variabel Button
    public string sceneName;

    void Start()
    {
        // Menetapkan callback untuk mendeteksi akhir video
        videoPlayer.loopPointReached += EndReached;

        // Memulai pemutaran video
        videoPlayer.Play();

        // Menambahkan fungsi onClick ke tombol skipButton
        skipButton.onClick.AddListener(SkipIntro);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        // Panggil metode untuk masuk ke dalam permainan (in-game)
        LoadInGameScene();
    }

    void SkipIntro()
    {
        // Panggil metode untuk melewati intro dan masuk ke dalam permainan (in-game)
        LoadInGameScene();
    }

    void LoadInGameScene()
    {
        // Ganti "YourInGameScene" dengan nama scene in-game Anda
        SceneManager.LoadScene(sceneName);
    }
}
