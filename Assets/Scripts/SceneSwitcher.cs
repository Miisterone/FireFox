using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int actualLevel;
    [SerializeField] private int nextLevel;
    private void Start()
    {
        actualLevel = SceneManager.GetActiveScene().buildIndex;
        nextLevel = actualLevel + 1;
    }

    public void NextLevel()
    {
        if (nextLevel >= SceneManager.sceneCountInBuildSettings)
            nextLevel = 1;
        
        LoadScene(nextLevel);
        UnloadScene(actualLevel);
    }
    
    private void LoadScene(int sceneName)
    {
        // Assurez-vous que la scène n'est pas déjà chargée
        if (!SceneManager.GetSceneByBuildIndex(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
    
    private void UnloadScene(int sceneName)
    {
        // Assurez-vous que la scène est actuellement chargée avant d'essayer de la décharger
        if (SceneManager.GetSceneByBuildIndex(sceneName).isLoaded)
        {
            SceneManager.UnloadScene(sceneName);
        }
    }
}
