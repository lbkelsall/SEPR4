using UnityEngine;
using UnityEditor;
using NUnit.Framework;

//Note that inorder to run these tests correctly any references saving or loading a gamestate should be commented out (in cycleplayers)
public class TurnManagerTesting {

	int numberOfPlayers = 2;

	TurnManager turnManager;
	GameState state1;
	GameState state2;
	GameState[] states = new GameState[2];

	[TestFixtureSetUp]
	public void TestSetup()
	{
		turnManager = new TurnManager (3,5f,numberOfPlayers);
		state1 = new GameState ( new PlayerCharacter("player1",null, null,null,null,null)); 
		state2 = new GameState ( new PlayerCharacter("player2",null, null,null,null,null)); 
		states[0] = state1;
		states [1] = state2;
		turnManager.SetStates (states,numberOfPlayers);
	}

	[Test]
	public void IncrementActionCounterTest(){
		int oldActionCounter = turnManager.GetActionCounter ();
		turnManager.IncrementActionCounter ();
		Assert.AreEqual (turnManager.GetActionCounter (), (oldActionCounter + 1));
	}


	[Test] //Action Counter as less than max:
	public void EndTurnCheckTestFalse(){
		turnManager.EndTurnCheck ();
		Assert.IsFalse(turnManager.HasPlayerSwitched ());
	}


	[Test] //Action counter greater than max (Need to comment out CyclePlayers to work)
	public void EndTurnCheckTestTrue(){
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.EndTurnCheck ();
		Assert.IsTrue(turnManager.HasPlayerSwitched ());
	}
		
	[Test] 
	public void GetPlayerTurnTest(){
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.EndTurnCheck ();
		Assert.AreEqual(2,turnManager.GetPlayerTurn ());
	}

	[Test] 
	public void HasPlayerSwitchedTest(){
		Assert.IsFalse(turnManager.HasPlayerSwitched ());
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.IncrementActionCounter ();
		turnManager.EndTurnCheck ();
		Assert.IsTrue(turnManager.HasPlayerSwitched ());
	}

	[Test] 
	public void SetStatesTest(){
		turnManager.SetStates (null,0);
		Assert.IsNull (turnManager.GetStates ());

		turnManager.SetStates (states,numberOfPlayers);
		Assert.AreSame(states,turnManager.GetStates ());
	}

	[Test] 
	public void TimerTest(){
		int oldTimer = turnManager.GetTimer ();
		turnManager.DecrementTimer ();
		Assert.IsTrue(turnManager.GetTimer () < oldTimer);
	}
}
