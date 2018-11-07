using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerName : NetworkBehaviour
{
    [SyncVar(hook = "UpdateName")]
    public string SyncedPlayerName;

    [SerializeField]
    private Text playerNameText;

    private InputField input;

    void Start()
    {
        this.UpdateName(this.SyncedPlayerName);

        if (isLocalPlayer)
        {
            this.input = FindObjectOfType<InputField>();
            input.onValueChanged.AddListener(text => this.CmdChangeName(text));
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        this.SyncedPlayerName = newName;
    }

    private void UpdateName(string name)
    {
        if (name.Equals(""))
        {
            name = "PLAYER " + Random.Range(1000, 9999).ToString();
        }
        this.playerNameText.text = name;
    }
}
