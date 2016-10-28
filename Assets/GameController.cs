using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	#region data
	private GameState state;
	public GameObject playerPrefab;
	public Text gameNote;
	private Canvas pMenu;
	public GameObject[] players = new GameObject[4];
	private Canvas[] pickle;
	public int prevPlayerIndex;
	public GameObject turnPlayer;
	private string tPlayerName;

	#endregion

	void Awake()
	{
		this.turnPlayer = null;
	}
	void Start () 
	{
		state = GameState.settingUp;
		tPlayerName = "Player1";
		foreach (Text t in pMenu.GetComponentsInChildren<Text> ())
			if (t.gameObject.tag.Equals ("Display"))
				gameNote = t;
		gameNote.text = tPlayerName + " turn: How many extra moves will you make?";
		pickle = GameObject.FindObjectsOfType<Canvas> ();
		foreach (Canvas c in pickle)
			if(c.gameObject.tag.Equals("Menu"))
				pMenu = c;
		pMenu.enabled = true;
		prevPlayerIndex = 0;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		Pause ();
	/*	switch (state) 
		{
		case GameState.paused:
			takeInput ();
			break;
		}*/
			//findTurnPlayer ();
			


		//turnPlayerGo ();
		//print (players.Length);
		
	}

	void Pause()
	{
		
		if (Input.GetKeyDown (KeyCode.Tab))
			if (pMenu.enabled)
				pMenu.enabled = false;
			else
				pMenu.enabled = true;
	}
	//Called in buttonScript that will deem how many players there will be
	public void adjustPlayers(int num)
	{
		players = new GameObject[num];
		for(int i = 0; i < num; i++)
			players[i] = (GameObject)Instantiate (playerPrefab, new Vector3 (i*2-3, 1, 0), Quaternion.identity);
		
		//Colors
		for (int i = 0; i < num; i++)
		{
			if(i == 0)
				players [i].GetComponent<MeshRenderer> ().material.color = Color.cyan;
			if(i == 1)
				players [i].GetComponent<MeshRenderer> ().material.color = Color.green;
			if(i == 2)
				players [i].GetComponent<MeshRenderer> ().material.color = Color.magenta;
			if(i == 3)
				players [i].GetComponent<MeshRenderer> ().material.color = Color.yellow;
		}

		//players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (Canvas c in pickle)
			if (c.gameObject.tag.Equals ("StartMenu"))    //***********************************************
				GameObject.Destroy (c);
		prevPlayerIndex = num - 1;
		findTurnPlayer ();
		print (this.turnPlayer.ToString () + "getturnplayer adjustplayers");
		//takeInput (); // Not gonna need this later

		//print (players.ToString());

	}

	public void changeState(GameState newState)
	{
		state = newState;
	}

	//Called in buttonScript
	public void takeInput(int value)
	{
		//players = GameObject.FindGameObjectsWithTag ("Player");
		print (players + "in take input");
		turnPlayer.GetComponent<Player> ().addMoves(value);
	}

	string turnPlayerName()
	{
		return tPlayerName;
	}

	public GameState getStateOfGame()
	{
		return state;
	}
	public void findTurnPlayer()
	{
		if (this.turnPlayer == null)
		{
			this.turnPlayer = players [1];
			prevPlayerIndex = 1;
			tPlayerName = "Player " + prevPlayerIndex + 1;
		}
		else if (prevPlayerIndex == players.Length - 1) 
		{
			this.turnPlayer = players [0];
			prevPlayerIndex = 0;
			tPlayerName = "Player " + prevPlayerIndex + 1;
		} 
		else 
		{
			this.turnPlayer = players [prevPlayerIndex + 1];
			prevPlayerIndex+=1;
			tPlayerName = "Player " + prevPlayerIndex + 1;
		}
		print (this.turnPlayer + "findturnplayer GC");
	}


	public GameObject getTurnPlayer()
	{
		return this.turnPlayer;
	}

}
