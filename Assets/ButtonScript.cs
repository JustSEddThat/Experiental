using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public int number = 0;
	public GameController gc;

	public void doThatThing() //For Start buttons
	{
		gc.adjustPlayers (number);
	}

	public void assignValue()
	{
			gc.takeInput(number);
	}

}
