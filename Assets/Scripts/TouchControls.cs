using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    private Player thePlayer;
    
	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Player>();
	}
	
	
    public void Kunai()
    {
        thePlayer.Kunai = true;
    }

    public void Jump()
    {
        if (thePlayer.OnGround)
            thePlayer.Jump = true;
    }
   
    public void Slide()
    {
        if (thePlayer.OnGround)
            thePlayer.Slide = true;
    }

    public void Restart()
    {
        if (GameController.instance.gameOver==true)
        {
            GameController.instance.Restart = true;
        }
    }
}
