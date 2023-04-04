using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NamEntryController : MonoBehaviour
{
public TMP_InputField inputField; // field in which player has entered player data

    private void OnEnable()
    {
        inputField.Select();
    }

    public void SetName()
    {
        GameStateMachine.SetPlayerName(inputField.text); // set name of player to the text in this field
    }

    private void OnDisable()
    {
        GameStateMachine.SetPlayerName(inputField.text);// set name of player to the text in this field
    }
}
