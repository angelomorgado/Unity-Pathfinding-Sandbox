using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load_scenes
{
    // Constructor
    public Load_scenes()
    {
        
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
