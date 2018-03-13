using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeResetScript : MonoBehaviour {

    public GhostAxeScript originalPosition;

    public void ResetAxe()
    {
        originalPosition.ResetAxe();
    }
}
