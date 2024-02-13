using System.Collections;
using Lean.Gui;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButton : MonoBehaviour
{
    private LeanButton refreshButton;

    private void Awake()
    {
        if (refreshButton == null)
            refreshButton = GetComponent<LeanButton>();
        
        refreshButton.OnClick.AddListener(Refresh);
    }

    private void Refresh()
    {
        StartCoroutine(RefreshWait());
    }

    private IEnumerator RefreshWait()
    {
        refreshButton.interactable = false;
        NetworkMenu.instance.RefreshSessionListUI();
        yield return new WaitForSeconds(3f);
        refreshButton.interactable = true;
    }
}
