using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// player attributs
	public float strength;
	public float torque;
	public Scoreboard score;

	// internal variables
	int rolls;
	int frame;

	// controllers
	BallController ball;
	PinsController pins;

	// state variable
	public enum State { WAIT, AIM, SHOOT, DONE, Length };
	private State _state;

	// state accessors
	public State state { 
		get {
			return _state;
		}
		private set {
			_state = value;
			EnterState();
		}
	}


	/*
	 *  Public methods
	 */
	
	public void Reset()
    { 
        score.Clear ();
		state = State.WAIT;
	}

	public void TakeTurn(int next_frame) 
	{
		rolls = 0;
		frame = next_frame;
		state = State.AIM;
	}


	/*
	 *  Private methods
	 */

	void Start()
	{
		score = new Scoreboard ();
		ball = GameObject.Find ("Ball").GetComponent<BallController> ();
		pins = GameObject.Find ("PinHolder").GetComponent<PinsController> ();
        Reset();
	}

    void EnterState() {
        switch (state)
        {
            case State.WAIT:
                ball.Reset();
                pins.Reset();

                break;

            case State.AIM:
                ball.Reset();
                pins.RemoveKnockedOut();
                if (pins.AllDown())
                    pins.Reset();
                break;

            case State.SHOOT:
                ball.Shoot(Vector3.forward, strength, torque);
                break;

            case State.DONE:
                rolls++;
                score.SetScore(frame, rolls, pins.KnockedOut());
                break;
        }
    }


	
	void Update() 
	{
        switch (state)
        {
            case State.WAIT:
                break;

            case State.AIM:
                ball.FollowMouse();
                if (Input.GetMouseButtonUp(0))
                    state = State.SHOOT;
                break;

            case State.SHOOT:
                if (ball.HasDone() && pins.HasDone())
                    state = State.DONE;
                break;

            case State.DONE:
                if (score.IsEnded(frame))
                    state = State.WAIT;
                else
                    state = State.AIM;
                break;
        }
    }
}
