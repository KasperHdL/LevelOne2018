using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSounds", menuName = "", order = 1)]
public class GameSoundClips : ScriptableObject {
	public AudioClip[] playerHit;
	public AudioClip[] dash;
	public AudioClip[] jump;
	public AudioClip[] land;
	public AudioClip[] push;
	public AudioClip[] dashCooldown;
	public AudioClip[] pushCooldown;
	public AudioClip[] death;
}
