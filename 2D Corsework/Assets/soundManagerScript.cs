using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    public static AudioClip PlayerHitSound, FireSound, EnemyDeathSound, FootstepsSound,JumpSound,PlayerSlashSound,SwordSwishSound,EnemyAttack,CoinCollect;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHitSound = Resources.Load<AudioClip>("playerHit");
        FireSound = Resources.Load<AudioClip>("fire");
        EnemyDeathSound = Resources.Load<AudioClip>("enemyDeath");
        FootstepsSound = Resources.Load<AudioClip>("footstep");
        JumpSound = Resources.Load<AudioClip>("jump");
        PlayerSlashSound = Resources.Load<AudioClip>("slash");
        SwordSwishSound = Resources.Load<AudioClip>("swordSwishing");
        EnemyAttack = Resources.Load<AudioClip>("EnemyAttack");
        CoinCollect = Resources.Load<AudioClip>("pick_up");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch(clip) { 
        case "fire":
            audioSrc.PlayOneShot(FireSound);
                break;
        case "playerHit":
            audioSrc.PlayOneShot(PlayerHitSound);
                break;
        case "jump":
            audioSrc.PlayOneShot(JumpSound);
                break;
        case "enemyDeath":
            audioSrc.PlayOneShot(EnemyDeathSound);
                break;
            case "footstep":
                audioSrc.PlayOneShot(FootstepsSound);
                break;
            case "slash":
                audioSrc.PlayOneShot(PlayerSlashSound);
                break;
            case "swordSwishing":
                audioSrc.PlayOneShot(SwordSwishSound);
                break;
            case "EnemyAttack":
                audioSrc.PlayOneShot(EnemyAttack);
                break;
            case "pick_up":
                audioSrc.PlayOneShot(CoinCollect);
                break;


        }
    }
}
