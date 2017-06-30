using UnityEngine;
using System.Collections;

public class ScoreboardGUI : MonoBehaviour
{	
	public GUIText gui;

	void Update () 
	{
		Scoreboard score = GetComponent<Player>().score;
        gui.text = GetFrames(score) + "\n" + GetScores(score) + " = " + score.GetTotal();

        TextMesh obj = GameObject.FindGameObjectWithTag("ecranScore").GetComponent<TextMesh>();
        obj.text = score.GetTotal().ToString();

	}

	string GetFrames(Scoreboard board) 
	{	
		string score = "| ";

		for (int f=0; f < 10; f++) 
		{
			Frame frame = board.scores[f];

			// 1st roll
			int v1 = frame.ball1;
			     if (v1 == -1) score += " ";
			else if (v1 ==  0) score += "-";
			else if (v1 == 10) score += "x";
			else               score += v1;

			// separator
			score += "  ";

			// 2nd roll
			if (frame.IsSpare())
			{
				score += "/";
			} 
			else 
			{
				int v2 = frame.ball2;
				     if (v2 == -1) score += " ";
				else if (v2 ==  0) score += "-";
				else if (v2 == 10) score += "x";
				else               score += v2;
			}

			// handle last frame
			if (f==9) 
			{
				int v3 = frame.ball3; 
				if(v3 != -1)
				{
					score += "  ";
					if (v3 ==  0) 
						score += "-";
					else if (v3 == 10)
						score += "x";
					else if( frame.IsStrike() && frame.ball2+v3 == 10 )
						score += "/";
					else
						score += v3;
				}
			}

			score += " | ";
		}
		return score;
	}

	string GetScores(Scoreboard board)
	{
		string score = "   ";

		for (int f=0; f < 10; f++) 
		{
			int total = board.scores[f].total;

			if(f==9 && board.scores[f].ball3 != -1)
				score += "   ";

			if (total < 0)
				score += "  ";
			else if(total<10)
				score += " " + total;
			else
				score += total;

			if(f<9)
				score += "     ";
			else
				score += " ";
		}

		return score;
	}
}
