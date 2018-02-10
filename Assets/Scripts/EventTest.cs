using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour 
{
	void Start()
	{
		GameEventHandler.Subscribe(GameEvent.PlayerHit, OnPlayerHit);
		GameEventHandler.Subscribe(GameEvent.Dash, OnDash);
		GameEventHandler.Subscribe(GameEvent.Jump, OnJump);
		GameEventHandler.Subscribe(GameEvent.Land, OnLand);
		GameEventHandler.Subscribe(GameEvent.Push, OnPush);
		GameEventHandler.Subscribe(GameEvent.CooldownDash, OnCooldownDash);
		GameEventHandler.Subscribe(GameEvent.CooldownPush, OnCooldownPush);
	}

	private void OnPlayerHit(GameEventArgs arguments)
	{
		Debug.Log("PlayerHit");
	}

	private void OnDash(GameEventArgs arguments)
	{
		Debug.Log("Dash");
	}

	private void OnJump(GameEventArgs arguments)
	{
		Debug.Log("Jump");
	}

	private void OnLand(GameEventArgs arguments)
	{
		Debug.Log("Land");
	}

	private void OnPush(GameEventArgs arguments)
	{
		Debug.Log("Push");
	}

	private void OnCooldownDash(GameEventArgs arguments)
	{
		Debug.Log("Cooldown Dash");
	}

	private void OnCooldownPush(GameEventArgs arguments)
	{
		Debug.Log("Cooldown Push");
	}
}