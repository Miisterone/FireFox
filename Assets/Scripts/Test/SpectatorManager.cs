using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpectatorManager : NetworkBehaviour
{
    public  NetworkRunner _runner;
    [Networked] 
    public string playerName { get; set; }
    public GameObject canvas;

    private void Awake() {
        _runner = GameObject.FindObjectOfType<NetworkRunner>();
    }
    
    public override void Spawned()
    {
        if (Runner.IsServer)
            Destroy(canvas);
        
        playerName = "SpectatorPlayer";
        name = playerName;
    }
    public void BackToMenu() {
        if (_runner)
            _runner.Shutdown();
        
        SceneManager.LoadScene("MobileMenu");
    }
}
