using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILivesText : MonoBehaviour
{
    private TextMeshProUGUI TMProText; // text field on object


    // Start is called before the first frame update
    private void Start()
    {
        TMProText = GetComponent<TextMeshProUGUI>();
        
        GameManager.Instance.onLivesChanged += HandleLivesChanged;
        TMProText.text = GameManager.Instance.lives.ToString();
    }
    private void OnDestroy()
    {
        GameManager.Instance.onLivesChanged -= HandleLivesChanged;
    }

    void HandleLivesChanged (int livesRemaining)
    {
        TMProText.text = livesRemaining.ToString(); // sets text field as remaining lives in string format
    }
}
