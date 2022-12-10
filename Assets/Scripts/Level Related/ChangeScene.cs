using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Database;
using System.Collections;
public class ChangeScene : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }
    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnButtonCheckerClick(Button button)
    {
        bool isNumeric = int.TryParse(button.name, out int n);

        if (isNumeric)
            Database.LevelRelated.SelectedLevelFromScene = n - 1;
    }
}