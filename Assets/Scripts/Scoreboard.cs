using UnityEngine;

// the score for a single frame (the value '-1' means that ball has not been rolled)
public class Frame 
{
	public int ball1; // pins down for ball 1
	public int ball2; // pins down for ball 2
	public int ball3; // pins down for ball 3 (only used in the last frame)
	public int total; // total score for this frame (may include future rolls)
	
	public void Clear() {
		ball1 = -1;
		ball2 = -1;
		ball3 = -1;
		total = -1;
	}

	public bool IsStrike() {
		return ball1 == 10;
	}

	public bool IsSpare() {
		return !IsStrike() && (ball1 + ball2 == 10);
	}
}

public class Scoreboard 
{
	public Frame[] scores;  // all 10 frames of the game

	public Scoreboard() 
	{
		scores = new Frame[10];
		for(int i=0; i<scores.Length; i++) {
			scores[i] = new Frame();
		}
		Clear ();
	}
	
	public void Clear()
	{
		foreach(var frame in scores) {
			frame.Clear();
		}
	}

	public bool IsEnded(int frame)
	{
		if( frame == 9 )
			return scores [frame].ball2 != -1 && !scores [frame].IsStrike () && !scores [frame].IsSpare () || scores [frame].ball3 != -1;
		else
			return scores [frame].ball2 != -1 || scores [frame].IsStrike ();
	}

	public void SetScore (int frame, int rolls, int count)
	{
		if( rolls == 1 )
			SetBall1(frame, count);
		else if( rolls == 2 )
			SetBall2(frame, count);
		else if( rolls == 3 )
			SetBall3(frame, count);
		else
			Debug.LogError("More than 3 rolls");
	}

	public int GetTotal()
	{
		int count = 0;
		for(int i = 0; i < scores.Length; i++)
		{
			if( scores[i].total > -1 )
				count += scores[i].total;
			else
				break;
		}
		return count;
	}

	public int GetScore(int frame)
	{
		if( scores[frame].total == -1 )
			return -1;
		else
		{
			// sum the total of all the frames
			int count = 0;
			for(int i = frame; i >= 0; i--)
				count += scores[i].total;

			return count;
		} 
	}
	
	void SetBall1(int frame, int pins) 
	{
		scores[frame].ball1 = pins;

		// if the previous frame was a spare, set its score
		if ( frame>0 && scores[frame-1].IsSpare() ) 
		{
			scores[frame-1].total = scores[frame-1].ball1 
				                  + scores[frame-1].ball2
					              + scores[frame  ].ball1;
		}

		// if the previous two frames were strikes, set the score of the former
		if ( frame>1 && scores[frame-1].IsStrike() && scores[frame-2].IsStrike() ) 
		{
			scores[frame-2].total = scores[frame-2].ball1 
				                  + scores[frame-1].ball1
					              + scores[frame  ].ball1;
		}
	}
	
	void SetBall2(int frame, int pins) 
	{
		scores[frame].ball2 = pins;

		// if this is not a spare or strike (in the last frame), set the score  
		if ( !scores[frame].IsSpare() && !scores[frame].IsStrike() ) {
			scores[frame].total = scores[frame].ball1 
				                + scores[frame].ball2;
		}

		// if the previous frame was a strike, set that score
		if ( frame>0 && scores[frame-1].IsStrike() ) 
		{
			scores[frame-1].total = scores[frame-1].ball1 
				                  + scores[frame  ].ball1
					              + scores[frame  ].ball2;
		}
	}
	
	void SetBall3(int frame, int pins) 
	{
		scores[frame].ball3 = pins;
		scores[frame].total = scores[frame].ball1 + scores[frame].ball2 + scores[frame].ball3;
	}
}