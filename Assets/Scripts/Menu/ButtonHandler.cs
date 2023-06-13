using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public Button button;
    public Dropdown_Controller dropdown_controller;
    public string choice;
    public int map_chosen;

    private void Start()
    {
        choice = button.name;

        dropdown_controller = new Dropdown_Controller();

        // Retrieve the stored algorithm choice value, or default to 0 if it doesn't exist
        PlayerPrefs.SetInt("AlgorithmChoice", 0);

        // Add a click listener to the button
        button.onClick.AddListener(ButtonClicked);
    }

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Scenes order in build settings:
    // 0 - Main menu
    // 1 - Menu settings
    // 2 - Cyber map
    // 3 - Space map
    // 4 - Playground map
    // 5 - Maze map
    // 6 - Sample mechanics

    // Algorithms order in dropdown:
    // 0 - A*
    // 1 - Dijkstra

    private void ButtonClicked()
    {
        switch (button.name)
        {
            case "Cyber_select":
                // Loads cyber map
                Debug.Log("Algoritm chosen: " + PlayerPrefs.GetInt("AlgorithmChoice", 0));
                SceneManager.LoadScene(2);
                lockCursor();
                break;

            case "Space_select":
                // Loads space map
                Debug.Log("Algoritm chosen: " + PlayerPrefs.GetInt("AlgorithmChoice", 0));
                SceneManager.LoadScene(3);
                lockCursor();
                break;

            case "Playground_select":
                // Loads playground map
                Debug.Log("Algoritm chosen: " + PlayerPrefs.GetInt("AlgorithmChoice", 0));
                SceneManager.LoadScene(4);
                lockCursor();
                break;

            case "Maze_select":
                // Loads maze map
                Debug.Log("Algoritm chosen: " + PlayerPrefs.GetInt("AlgorithmChoice", 0));
                SceneManager.LoadScene(5);
                lockCursor();
                break;

            case "Exit":
                // Load main menu
                SceneManager.LoadScene(0);
                break;

            case "Start":
                // Load chosen map
                SceneManager.LoadScene(1);
                break;

            case "End":
                // Stops the game
                Application.Quit();
                break;
        }
    } 
}
