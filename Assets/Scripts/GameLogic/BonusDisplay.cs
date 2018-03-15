using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusDisplay : MonoBehaviour {

    public Animator anim;
    public Text displayText;
    
    public void TriggerAnim(string newText = "")
    {
        if (newText != "") displayText.text = newText;
        anim.SetTrigger("ShowBonus");
    }
}
