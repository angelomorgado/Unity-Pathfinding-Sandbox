using UnityEngine;
using TMPro;

public class Dropdown_Controller : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public int chosen_algorithm;

    public Dropdown_Controller()
    {
        
    }

    private void Start()
    {
        // Add listener for when the value of the Dropdown changes
        dropdown.onValueChanged.AddListener(delegate 
        { 
            OnDropdownValueChanged(dropdown); 
        });
    }

    private void OnDropdownValueChanged(TMPro.TMP_Dropdown dropdown)
    {
        chosen_algorithm = dropdown.value;
        PlayerPrefs.SetInt("AlgorithmChoice", chosen_algorithm);
        PlayerPrefs.Save();
        
        // Use refreshShownValue if you want to change the current value
        dropdown.RefreshShownValue();
    }
}
