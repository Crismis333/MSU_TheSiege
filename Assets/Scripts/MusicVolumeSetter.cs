using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicVolumeSetter : MonoBehaviour {

	void Start () {
        GetComponent<AudioSource>().volume = 0;
        GetComponent<AudioSource>().Play();
	}
}
