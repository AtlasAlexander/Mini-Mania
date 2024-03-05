using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    public List<Button> buttons;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
            PlayerPrefs.SetInt("Level", 1);

        //Set starting button colour
        if (buttons.Count > 0)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level"); i++)
            {
                buttons[i].image.color = Color.white;
            }

            for (int i = PlayerPrefs.GetInt("Level"); i < buttons.Count; i++)
            {
                buttons[i].image.color = Color.grey;
                buttons[i].interactable = false;
            }
        }
    }

    public void LoadLevel(int num)
    {
        PlayerPrefs.SetInt("Checkpoint", 0);
        SceneManager.LoadScene(num + 1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
