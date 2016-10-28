using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

	Vector3 initialPosition;
	public bool doneMoving;
	public float numSteps = 4;
	public GameController gc;
	private Camera cam;
	void Start () 
	{
		initialPosition = transform.position;
		doneMoving = false;
		cam = gameObject.GetComponentInChildren<Camera> ();
		cam.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(gc.turnPlayer.Equals(this.gameObject))
		{
			cam.gameObject.SetActive (true);
			print (gc.getStateOfGame () + "player update" + gc.turnPlayer);


			if (gc.getStateOfGame () == GameState.playing)
				moveForward ();
		}
		else
			cam.gameObject.SetActive (false);
		
	}

	void moveForward()
	{
		if(Vector3.Distance(initialPosition, transform.position) < numSteps*10)
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 10), 8 * Time.deltaTime);
		if (Vector3.Distance (initialPosition, transform.position) >= numSteps * 10)
			playerDone ();
	}


	public void playerDone()
	{
		gc.changeState (GameState.paused);
		numSteps = 4; //reset natural numSteps
		gc.findTurnPlayer ();
	}

	public void addMoves(int number)
	{
		numSteps += number;
		gc.changeState (GameState.playing);
	}


		
}
