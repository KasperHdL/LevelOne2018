using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour 
{
	public GameObject jumpParticles;
	public GameObject dashParticles;
	public GameObject playerHitParticles;
	public GameObject landParticles;
	public GameObject pushParticles;
	public GameObject cooldownDashParticles;
	public GameObject cooldownPushParticles;

	void Start()
	{
		GameEventHandler.Subscribe(GameEvent.CooldownDash, OnCooldownDash);
		GameEventHandler.Subscribe(GameEvent.CooldownPush, OnCooldownPush);
		GameEventHandler.Subscribe(GameEvent.PlayerHit, OnPlayerHit);
		GameEventHandler.Subscribe(GameEvent.Dash, OnDash);
		GameEventHandler.Subscribe(GameEvent.Jump, OnJump);
		GameEventHandler.Subscribe(GameEvent.Land, OnLand);
		GameEventHandler.Subscribe(GameEvent.Push, OnPush);	
	}

	private void OnPlayerHit(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;

		ParticleSystem ps = playerHitParticles.GetComponent<ParticleSystem>();
		playerHitParticles.SetActive(true);
		
		playerHitParticles.transform.position = playerArguments.position;

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnDash(GameEventArgs arguments)
	{
		ParticleSystem ps = dashParticles.GetComponent<ParticleSystem>();
		dashParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnJump(GameEventArgs arguments)
	{
		ParticleSystem ps = jumpParticles.GetComponent<ParticleSystem>();
		jumpParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnLand(GameEventArgs arguments)
	{
		ParticleSystem ps = landParticles.GetComponent<ParticleSystem>();
		landParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnPush(GameEventArgs arguments)
	{
		ParticleSystem ps = pushParticles.GetComponent<ParticleSystem>();
		pushParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnCooldownDash(GameEventArgs arguments)
	{
		ParticleSystem ps = cooldownDashParticles.GetComponent<ParticleSystem>();
		cooldownDashParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}

	private void OnCooldownPush(GameEventArgs arguments)
	{
		ParticleSystem ps = cooldownPushParticles.GetComponent<ParticleSystem>();
		cooldownPushParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(ps);
        StartCoroutine(coroutine);
	}
	
	private IEnumerator DisableParticlesystemOnEndPlay(ParticleSystem ps)
	{
		while(ps.isPlaying)
		{
			yield return null;
		}

		ps.gameObject.SetActive(false);
	}
}
