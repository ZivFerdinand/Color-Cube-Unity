using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Database;

public class ChangeScene : MonoBehaviour
{
    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }
    public void onCloseButtonClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void onButtonCheckerClick(Button button)
    {
        int n;
        bool isNumeric = int.TryParse(button.name, out n);

        if(isNumeric)
            Database.LevelRelated.selectedLevelFromScene = int.Parse(button.name);
    }
}