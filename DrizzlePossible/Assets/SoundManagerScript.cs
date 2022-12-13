using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManagerScript : MonoBehaviour
{
    [Header("Set in Inspector")]
    public static AudioClip playerFireSound, dyingSound, powerUpSound, coinSound,chestSound,enemyHitSound ;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerFireSound = Resources.Load<AudioClip>("Laser");
        dyingSound = Resources.Load<AudioClip>("explosion");
        powerUpSound = Resources.Load<AudioClip>("powerUp");
        coinSound = Resources.Load<AudioClip>("pickupCoin");
        chestSound = Resources.Load <AudioClip>("Chest Opening");
        enemyHitSound = Resources.Load<AudioClip>("enemyHit");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "fire":
                audioSrc.PlayOneShot(playerFireSound);
                break;
            case "death":
                audioSrc.PlayOneShot(dyingSound);
                break;
            case "powerUp":
                audioSrc.PlayOneShot(powerUpSound);
                break;
            case "coin":
                audioSrc.PlayOneShot(coinSound);
                break;
            case "chest":
                audioSrc.PlayOneShot(chestSound);
                break;
            case "enemyhit":
                audioSrc.PlayOneShot(enemyHitSound);
                break;


        }
    }
}