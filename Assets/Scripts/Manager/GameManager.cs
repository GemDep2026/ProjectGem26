using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool cutscenePlayed = false; // Variabel status cutscene

    void Awake()
    {
        // Pastikan hanya satu objek GameManager yang ada di dalam scene
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Biarkan objek hidup melalui pergantian scene
        }
    }
}
