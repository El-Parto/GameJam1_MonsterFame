using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetingOptions : MonoBehaviour
{
	public void LimitFramerate(int target)
	{
		target = 60;
		Application.targetFrameRate = target;
		
		//resolution stuff below
		Resolution[] allowedRes = Screen.resolutions;

		Resolution res = new Resolution();

		/*
		 // use allowed Res to fill up a dropdown in unity
		 screem.currentresolution = allowedRes[0]
		var example = from s in allowedRes
		              where s.refreshRate == 30
		              select s.height;
		*/
		res.height = 1080;
		res.width = 1920;
		res.refreshRate = Screen.currentResolution.refreshRate;

	}
}
