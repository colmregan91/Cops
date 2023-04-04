using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameManager.Instance.onCoinPickedUp += PlayCoinPickupAudio;
    }
    private void OnDestroy()
    {
        GameManager.Instance.onCoinPickedUp -= PlayCoinPickupAudio;
    }

    void PlayCoinPickupAudio (int coins)
    {
        audioSource.Play();
    }
}
