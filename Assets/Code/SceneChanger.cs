using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeTo(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
