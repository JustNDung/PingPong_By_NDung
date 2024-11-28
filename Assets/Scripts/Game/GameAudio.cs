using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource asSounds;
    //public AudioClip wallSound;
    //public AudioClip paddleSound;
    public AudioClip scoreSound;
    public AudioClip winSound;
    public void PlayScoreSound(){
        asSounds.PlayOneShot(scoreSound);
    }
    public void PlayWinSound(){
        asSounds.PlayOneShot(winSound);
    }
}
