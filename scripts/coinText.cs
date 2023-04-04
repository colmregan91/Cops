using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class coinText : MonoBehaviour
{
    private TextMeshProUGUI TMProText;

    // Start is called before the first frame update
    void Start()
    {
        TMProText = GetComponent<TextMeshProUGUI>();
        TMProText.text = GameManager.Instance.getCoins().ToString();
        GameManager.Instance.onCoinPickedUp += HandleCoinPickedUp;
    }


    private void OnDestroy()
    {
        GameManager.Instance.onCoinPickedUp -= HandleCoinPickedUp;
    }

    void HandleCoinPickedUp(int coins)
    {
        TMProText.text = coins.ToString();
        if (coins >= 10) // if player hits 10 coins
        {
            GameManager.Instance.AddLife();
        }
       
    }

}
