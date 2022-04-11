using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip JumpSound, DeathSound, AcquireMagenetSound, CheckPointSound, DropSound, PassLevelSound, MagenetFieldSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        JumpSound = Resources.Load<AudioClip>("DM-CGS-07");
        DeathSound = Resources.Load<AudioClip>("DM-CGS-11");
        AcquireMagenetSound = Resources.Load<AudioClip>("DM-CGS-33");
        CheckPointSound = Resources.Load<AudioClip>("DM-CGS-26");
        DropSound = Resources.Load<AudioClip>("DM-CGS-46");
        PassLevelSound = Resources.Load<AudioClip>("DM-CGS-18");
        MagenetFieldSound = Resources.Load<AudioClip>("DM-CGS-40");
    

        audioSrc = GetComponent<AudioSource>();

    }

    public static void PlaySound(string clip)
    {
        switch(clip){
            case "jump":
                audioSrc.PlayOneShot(JumpSound);
                break;
            case "death":
                audioSrc.PlayOneShot(DeathSound);
                break;
            case "acquire":
                audioSrc.PlayOneShot(AcquireMagenetSound);
                break;
            case "checkpoint":
                audioSrc.PlayOneShot(CheckPointSound);
                break;
            case "drop":
                audioSrc.PlayOneShot(DropSound);
                break;
            case "pass":
                audioSrc.PlayOneShot(PassLevelSound);
                break;
            case "magenetfield":
                audioSrc.PlayOneShot(MagenetFieldSound);
                break;
        }




    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
