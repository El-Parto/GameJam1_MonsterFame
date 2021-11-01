using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.Apple;


public class SetingOptions : MonoBehaviour
{
	public Resolution[] resolutions;
	public TMP_Dropdown resDrop;
	
	private List<string> options = new List<string>();
	private int currentResolutionIndex = 0;
	/*
	public TMP_Dropdown frameDrop;
	private float[] framerates = { 10, 15, 24, 30, 60, 90};
	private List<string> fpsOptions = new List<string>();
	private float currentFramerate;
	private int fpsIndex;
	*/
	[SerializeField] private Slider fpsSlider;
	[SerializeField] private TextMeshProUGUI fpsText;
	[SerializeField] private TextMeshProUGUI currentFPStext;
	/// <summary>
	/// Limits framerate according to a Slider value
	/// </summary>
	public void LimitFramerate()
	{
		fpsSlider.wholeNumbers = true;
		int min = 3;
		int max = 121;
		fpsSlider.minValue = min;
		fpsSlider.maxValue = max;
		if(fpsSlider.value == 121)
		{
			fpsSlider.value = -1;
		}
		

		Application.targetFrameRate = Mathf.RoundToInt(fpsSlider.value);
		fpsText.text = fpsSlider.value.ToString("F0");


/*
		for(int i = 0; i < framerates.Length; i++)
		{
			string fps = $"{framerates[i]}fps";
			fpsOptions.Add(fps);
			if(currentFramerate == Application.targetFrameRate)
			{
				fpsIndex = i;
			}

			



		}

		int max = -1;
		string noLimit = $"{max}No Limit";
		fpsOptions.Add(noLimit);

		frameDrop.AddOptions(fpsOptions);
		frameDrop.RefreshShownValue();*/
/*

	Resolution res = new Resolution();


	 // use allowed Res to fill up a dropdown in unity
	 screem.currentresolution = allowedRes[0]
	var example = from s in allowedRes
	              where s.refreshRate == 30
	              select s.height;
	
		res.height = 1080;
		res.width = 1920;
		res.refreshRate = Screen.currentResolution.refreshRate;
*/
	}

	
	private void Start()
	{

		StartResFPS();
		LimitFramerate();

		
	}

/// <summary>
/// shows the resolution and FPS controls on screen like an adjustable hud.
/// </summary>
	private void StartResFPS()
	{
		resolutions = Screen.resolutions;
		resDrop.ClearOptions();
		
		
		for(int i=0; i <resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);
			if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
			{
				currentResolutionIndex = i;
			}
		}
		resDrop.AddOptions(options);
		resDrop.value = currentResolutionIndex;
		resDrop.RefreshShownValue();	

		
		
	}

	public void Update() => currentFPStext.text = $"{(1.0f / Time.smoothDeltaTime).ToString("F0")}FPS";
	
/// <summary>
/// sets the resolution based on the option chesen
/// </summary>
/// <param name="_resolution">The resolutions that the computer can reach.</param>
	public void SetResolution(int _resolution)
	{
		Resolution neverReachedA = resolutions[_resolution];
		Screen.SetResolution(neverReachedA.width, neverReachedA.height, Screen.fullScreen);
	}
}
