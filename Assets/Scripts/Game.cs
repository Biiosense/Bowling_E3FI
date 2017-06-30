using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
	public Player player;

	// state variables
	bool show;
	int frame;

	void Start() {
		show = true;
		frame = -1;
	}

	public void Play()
	{
		show = false;
		frame = 0; 
		player.Reset ();
	}

	void Update () 
	{
        if (player.state == Player.State.WAIT)
        {
            if (frame == 10)
            {
                frame = -1;
                show = true;
            }
            if (frame > -1)
            {
                player.TakeTurn(frame);
                frame++;
            }
        }
	}

	void OnGUI () 
	{
		if(show)
		{
			GUI.BeginGroup( new Rect(Screen.width/2-50,Screen.height/2-100,100,100) );
			GUI.Box (new Rect (0,0,100,100), "Menu");
			
			// Make the first button
			if( GUI.Button(new Rect (10,25,80,30), "Play" ) ) {
                Input.ResetInputAxes();
                Play();
				show = false;
			}
			
			// Make the second button.
			if( GUI.Button(new Rect (10,60,80,30), "Quit") ) {
				Application.Quit();
			}
			
			GUI.EndGroup();
		}
	}
}