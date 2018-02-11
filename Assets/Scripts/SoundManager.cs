using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public GameSoundClips gameSound;
	public float volume = 1.0f;

	void Start()
	{
		GameEventHandler.Subscribe(GameEvent.CooldownDash, OnCooldownDash);
		GameEventHandler.Subscribe(GameEvent.CooldownPush, OnCooldownPush);
		GameEventHandler.Subscribe(GameEvent.PlayerHit, OnPlayerHit);
		GameEventHandler.Subscribe(GameEvent.Dash, OnDash);
		GameEventHandler.Subscribe(GameEvent.Jump, OnJump);
		GameEventHandler.Subscribe(GameEvent.Land, OnLand);
		GameEventHandler.Subscribe(GameEvent.Push, OnPush);	
		GameEventHandler.Subscribe(GameEvent.PlayerDeath, OnDeath);
	}
	
	private void OnDeath(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.death);
	}

	private void OnPlayerHit(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.playerHit);
	}

	private void OnDash(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.dash);
	}

	private void OnJump(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.jump);
	}

	private void OnLand(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.land);
	}

	private void OnPush(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.push);
	}

	private void OnCooldownDash(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.dashCooldown);
	}

	private void OnCooldownPush(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		PlayRandomSoundAtPoint(playerArguments.position, gameSound.pushCooldown);
	}

	private void PlayRandomSoundAtPoint(Vector3 position, AudioClip[] sound)
	{
		int rnd = Random.Range(0, sound.Length-1);

		AudioSource.PlayClipAtPoint(sound[rnd], position, volume);
	}
}
