// path/filename: SimpleSceneSwitcher.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;
using System.Collections.Generic;

public class SimpleSceneSwitcher : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnSceneIndexChanged))]
    public string CurrentSceneName { get; set; }

    // Use a list of strings for scene names
    public List<string> sceneNames = new List<string>();

    public override void Spawned()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && Runner.IsServer)
        {
            CycleToNextScene();
        }
    }

    private void CycleToNextScene()
    {
        int currentIndex = sceneNames.IndexOf(CurrentSceneName);
        int nextIndex = (currentIndex + 1) % sceneNames.Count;
        SetScene(sceneNames[nextIndex]);
    }

    private void SetScene(string sceneName)
    {
        if (CurrentSceneName != sceneName)
        {
            CurrentSceneName = sceneName;
        }
    }

    private static void OnSceneIndexChanged(Changed<SimpleSceneSwitcher> changed)
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if (activeSceneName != changed.Behaviour.CurrentSceneName)
        {
            changed.Behaviour.LoadScene(changed.Behaviour.CurrentSceneName);
        }
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}