using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME_MENU : MonoBehaviour
{
    // Method to start the game by loading the GhostGame scene
    public void play()
    {
        SceneManager.LoadScene("GhostGame");  // Replace with your actual scene name if different
    }

    // Method to load the level settings scene
    public void LoadLevelSettings()
    {
        SceneManager.LoadScene("LevelSettingsScene");  // Replace with your actual scene name
    }

    // Method to quit the game
    public void endgame()
    {
        Debug.Log("END");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
