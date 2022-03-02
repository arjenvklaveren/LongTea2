using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NameGeneration : MonoBehaviour
{
    private RoomPlayerUI owner;
    [SerializeField] private TMPro.TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FetchPlayerUI", 1f);
        Invoke("UpdateName", 1.5f);
    }

    private void FetchPlayerUI()
    {
        RoomPlayerUI[] playerUI = GameObject.FindObjectsOfType<RoomPlayerUI>();

        Debug.Log("Doing things with players and such");
        foreach (RoomPlayerUI playerUIInstance in playerUI)
        {
            Debug.Log(playerUIInstance.playerName);
            if (playerUIInstance.hasAuthority)
            {
                owner = playerUIInstance;
                Debug.Log("Owner is " + playerUIInstance.playerName);
                break;
            }
        }
    }

    public void UpdateNameToTextField()
    {
        UpdateName(inputField.text);
    }

    public void UpdateName(string newName = "")
    {
        if (string.IsNullOrEmpty(newName))
            if (owner && inputField) inputField.SetTextWithoutNotify(owner.SetRandomPlayerName());

        owner.ChangePlayerName(inputField.text);
    }
}
