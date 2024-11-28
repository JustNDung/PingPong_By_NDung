using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Animator animator;
    public void GetScore(int value){
        text.text = value.ToString();
    }
    public void Highlight(){
        animator.SetTrigger("highlight");
    }
}
