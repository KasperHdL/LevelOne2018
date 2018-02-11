using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSounds", menuName = "", order = 1)]
public class GameSoundClips : ScriptableObject {
	[Header("Soundtrack")]
	public AudioClip soundtrack;

	[Header("PlayerSounds")]
	public AudioClip[] playerHit;
	public AudioClip[] dash;
	public AudioClip[] jump;
	public AudioClip[] land;
	public AudioClip[] push;
	public AudioClip[] dashCooldown;
	public AudioClip[] pushCooldown;
	public AudioClip[] death;

	[Header("Announcer Sounds")]
	public AudioClip[] deathAnnouncings;
	public AudioClip[] countdown;
	public AudioClip[] gameStart;
}
