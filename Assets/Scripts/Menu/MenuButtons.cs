using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private MenuBehaviour menu;
    [SerializeField] private TaskList tasks;
    [SerializeField] private GameObject startDarkBox;

    public void UseButton(string button)
    {
        Debug.Log("Used Button " + button);
        if (button.Equals("Start") || button.Equals("Restart"))
            StartGame();

        if (button.Equals("Options"))
            OptionsGame();

        if (button.Equals("Quit"))
            QuitGame();
    }

    void StartGame()
    {
        menu.followCamera = false;
        tasks.ResetLevel();
        GameController._.ReportAction(GameController.Action.StartGame);
        Destroy(startDarkBox);
    }

    void QuitGame()
    {
        menu.followCamera = true;
    }

    void OptionsGame()
    {
        //menu.followCamera = false;
    }
}
