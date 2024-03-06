using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endpoint : MonoBehaviour
{
    private PlayerFarestPos EndDetect;

    private void Start()
    {
        EndDetect = FindObjectOfType<PlayerFarestPos>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().PlayOneShot(AudioManager.instance.EndSound);
            EndDetect.OnGameEnd();

            GetComponent<Collider2D>().enabled = false;
        }
    }
}
