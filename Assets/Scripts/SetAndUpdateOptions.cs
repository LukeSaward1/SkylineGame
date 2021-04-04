using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Discord;

public class SetAndUpdateOptions : MonoBehaviour
{
	public Slider audioLevel;
	public Slider camSens;
	public Camera PlayerCamera;
	public PlayerMovement pM;

	public void SetCameraSpeed(float value)
	{
		pM.sensitivity = value;
	}

	public void SetAudioVolume(float value)
	{
		AudioListener.volume = value;
	}

	private void Start()
	{
		camSens.value = pM.sensitivity;
		audioLevel.value = AudioListener.volume;
	}

	private void Update()
	{

	}
}