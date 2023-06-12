using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public Button button;
    public string choice;
    public int map_chosen;
    private Load_scenes load;

    // constructor
    public ButtonHandler(Button button)
    {
        button = button;
        choice = button.name;
    }

    private void Start()
    {
        choice = button.name;

        load = new Load_scenes();

        // Retrieve the stored map_chosen value, or default to -1 if it doesn't exist
        map_chosen = PlayerPrefs.GetInt("MapChoice", 2);
        Debug.Log("map_chosen: " + map_chosen);

        // Add a click listener to the button
        button.onClick.AddListener(ButtonClicked);
    }

    private void savingMapChosen(int map_chosen)
    {
        // Store the map_chosen value in PlayerPrefs
        Debug.Log("Saving map_chosen: " + map_chosen);
        PlayerPrefs.SetInt("MapChoice", map_chosen);
        PlayerPrefs.Save();
    }

    // Scenes order in build settings:
    // 0 - Main menu
    // 1 - Menu settings
    // 2 - Cyber map
    // 3 - Space map
    // Maze map (TODO)
    // 4 - Sample mechanics

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

            case "Maze_select":
                // TODO
                // map_chosen = 4;
                break;

            case "Exit":
                // Load main menu
                load.LoadScene(0);
                break;

            case "Settings":
                // Load settings menu
                load.LoadScene(1);
                break;

            case "Start":
                // Load chosen map
                load.LoadScene(map_chosen);
                break;

            case "End":
                // Stops the game
                Application.Quit();
                break;
        }
    } 
}
