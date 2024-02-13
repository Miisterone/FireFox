using Lean.Gui;
using TMPro;
using UnityEngine;

public class SessionEntryPrefab : MonoBehaviour
{
   public TMP_Text sessionName, playerCount;
   public LeanButton joinButton;

   private void Awake()
   {
      joinButton.OnClick.AddListener(JoinSession);
   }

   private void JoinSession()
   {
      NetworkMenu.instance.ConnectToSession(sessionName.text);
   }
}
