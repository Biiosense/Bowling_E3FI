using UnityEngine;
using System.Collections;

public class HelpGUI : MonoBehaviour 
{
	public GUIText gui;
	
	void Update () 
	{
		gui.text  = "INSTRUCTIONS:\n" + getInstructions();
	}
	
	private string getInstructions ()
	{
		string text = "";

		Player player = GetComponent<Player> ();
		if (player.state == Player.State.WAIT) {
			text = "  0. Wait for your turn.";
		}
		else if (player.state == Player.State.AIM) {
			text = "  1. Click to throw.";
		} 
		else if (player.state == Player.State.SHOOT) {
			text = "  2. Wait until the shoot is completed.";
		}
		else if (player.state == Player.State.DONE) {
			text = "  3. Click to proceed.";
		}
		
		return text;
	}
}
