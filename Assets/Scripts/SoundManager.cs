﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public GameSoundClips gameSound;
	public AudioSource announcer;
	public AudioSource music;
	public float volume = 1.0f;
	public float musicVolume = 1.0f;

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
		GameEventHandler.Subscribe(GameEvent.GameStarted, AnnounceGo);
		GameEventHandler.Subscribe(GameEvent.GameCountdown, CountDown);

		StartCoroutine("FadeInMusic");
	}

	private IEnumerator FadeInMusic()
	{
		music.clip = gameSound.soundtrack;
		music.volume = 0;
		music.Play();
		float currentVolume = 0;
		float volumeIncrease = 0.1f;

		while(currentVolume < musicVolume)
		{
			currentVolume += volumeIncrease * Time.deltaTime;
			music.volume = currentVolume;
			yield return null;
		}
	}
	
	private void OnDeath(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		
		PlayRandomAnnouncerClip(gameSound.deathAnnouncings);

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

	private void CountDown(GameEventArgs arguments)
	{
		CountdownArgs countdownArgs = (CountdownArgs) arguments;

		if (countdownArgs.count >= gameSound.countdown.Length || countdownArgs.count < 0)
		{
			return;
		}

		PlayAnnouncerClip(gameSound.countdown[countdownArgs.count]);	
	}

	private void AnnounceGo(GameEventArgs Arguments)
	{
		announcer.Stop();
		PlayRandomAnnouncerClip(gameSound.gameStart);
	}

	private void PlayRandomSoundAtPoint(Vector3 position, AudioClip[] sound)
	{
		int rnd = Random.Range(0, sound.Length-1);

		AudioSource.PlayClipAtPoint(sound[rnd], position, volume);
	}

	private void PlayAnnouncerClip(AudioClip sound)
	{
		if(announcer.isPlaying)
		{
			announcer.Stop();
		}
	
		announcer.clip = sound;
		announcer.Play();
	}

	private void PlayRandomAnnouncerClip(AudioClip[] sound)
	{
		if(announcer.isPlaying)
		{
			return;
		}

		int rnd = Random.Range(0, sound.Length-1);
	
		announcer.clip = sound[rnd];
		announcer.Play();
	}
}
