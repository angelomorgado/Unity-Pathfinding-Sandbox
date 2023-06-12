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
    public int map_chosen, algorithm_chosen;

    private void Start()
    {
        choice = button.name;

        dropdown_controller = new Dropdown_Controller();

        // Retrieve the stored map_chosen value, or default to -1 if it doesn't exist
        map_chosen = PlayerPrefs.GetInt("MapChoice", 2);

        // Retrieve the stored algorithm_chosen value, or default to 0 if it doesn't exist
        algorithm_chosen = PlayerPrefs.GetInt("AlgorithmChoice", 0);

        // Add a click listener to the button
        button.onClick.AddListener(ButtonClicked);
    }

    private void savingMapChosen(int map_chosen)
    {
        // Store the map_chosen value in PlayerPrefs
        PlayerPrefs.SetInt("MapChoice", map_chosen);
        PlayerPrefs.Save();
    }

    private void savingAlgorithmChosen(int algorithm_chosen)
    {
        // Store the algorithm_chosen value in PlayerPrefs
        PlayerPrefs.SetInt("AlgorithmChoice", algorithm_chosen);
        PlayerPrefs.Save();
    }

    // Scenes order in build settings:
    // 0 - Main menu
    // 1 - Menu settings
    // 2 - Cyber map
    // 3 - Space map
    // Maze map (TODO)
    // 4 - Sample mechanics

    // Algorithms order in dropdown:
    // 0 - A*
    // 1 - Dijkstra

    private void ButtonClicked()
    {
        switch (button.name)
        {
            case "Cyber_select":
                savingMapChosen(2);
                break;

            case "Space_select":
                savingMapChosen(3);
                break;

            case "Playground_select":
                savingMapChosen(4);
                break;

            case "Maze_select":
                // TODO
                // savingMapChosen(5);
                break;

            case "Exit":
                // Load main menu
                SceneManager.LoadScene(0);
                break;

            case "Settings":
                // Load settings menu
                SceneManager.LoadScene(1);
                break;

            case "Start":
                // Load chosen map
                SceneManager.LoadScene(map_chosen);
                break;

            case "End":
                // Stops the game
                Application.Quit();
                break;
        }
    } 
}
