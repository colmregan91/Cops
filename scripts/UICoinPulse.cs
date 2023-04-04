using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoinPulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onCoinPickedUp += HandleCoinPickedUpUIPulse;
    }

    private void OnDestroy()
    {
        GameManager.Instance.onCoinPickedUp -= HandleCoinPickedUpUIPulse;

    }


    void HandleCoinPickedUpUIPulse(int unusedInt)
    {
       // Animator.SetTrigger("GotCoin");
    }
}
