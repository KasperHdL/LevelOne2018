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
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = playerHitParticles.GetComponent<ParticleSystem>();
		playerHitParticles.SetActive(true);
		
		playerHitParticles.transform.position = playerArguments.position;

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(playerHitParticles);
        StartCoroutine(coroutine);
	}

	private void OnDash(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = dashParticles.GetComponent<ParticleSystem>();
		dashParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(dashParticles);
        StartCoroutine(coroutine);
	}

	private void OnJump(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = jumpParticles.GetComponent<ParticleSystem>();
		jumpParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(jumpParticles);
        StartCoroutine(coroutine);
	}

	private void OnLand(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = landParticles.GetComponent<ParticleSystem>();
		landParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(landParticles);
        StartCoroutine(coroutine);
	}

	private void OnPush(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = pushParticles.transform.GetChild(0).GetComponent<ParticleSystem>();
		pushParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(pushParticles);
        StartCoroutine(coroutine);
	}

	private void OnCooldownDash(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = cooldownDashParticles.transform.GetChild(0).GetComponent<ParticleSystem>();
		cooldownDashParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(cooldownDashParticles);
        StartCoroutine(coroutine);
	}

	private void OnCooldownPush(GameEventArgs arguments)
	{
		PlayerEventArgument playerArguments = (PlayerEventArgument) arguments;
		if (playerArguments.gameObject != gameObject)
		{
			return;
		}

		ParticleSystem ps = cooldownPushParticles.GetComponent<ParticleSystem>();
		cooldownPushParticles.SetActive(true);

		IEnumerator coroutine = DisableParticlesystemOnEndPlay(cooldownPushParticles);
        StartCoroutine(coroutine);
	}
	
	private IEnumerator DisableParticlesystemOnEndPlay(GameObject particleObject)
	{
		ParticleSystem ps = particleObject.GetComponent<ParticleSystem>();
		if (ps == null)
		{
			ps = particleObject.transform.GetChild(0).GetComponent<ParticleSystem>();
		}

		while(ps.isPlaying)
		{
			yield return null;
		}

		particleObject.SetActive(false);
	}
}
