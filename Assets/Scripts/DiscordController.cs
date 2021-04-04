using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
	public Discord.Discord discord;
	public long clientID;
	public TimerController timerController;
	public string inScene;
	public string inLevel;
	public LevelManager LM;
	public string TheStatus;
	public string theTime;
	public string MainMenuName;
	public bool RichPresenceEnabled;

	// Use this for initialization
	public void Start()
	{
		discord = new Discord.Discord(clientID, (System.UInt64)Discord.CreateFlags.Default);
	}

	// Update is called once per frame
	public void Update()
	{
		MainMenuName = "Main Menu";
		inScene = "In " + MainMenuName;
		inLevel =  "In Level " + LM.levelID;

		var activityManager = discord.GetActivityManager();

		var activity = new Discord.Activity
		{
			Details = inScene + " | " + theTime,
			State = "Beta",
			Assets =
			{
				LargeImage = "bean",
				LargeText = "Skyline is a first-person platformer game made by Luke Saward in 2021.",
			},
		};

		if(timerController.IGTCounter.text == "00:00.000")
		{
			theTime = "Not Running";
		}
		else{
			theTime = timerController.IGTCounter.text;
		}

		if(LM.levelID >= 1)
		{
			activity.Details = inLevel + " | " + timerController.IGTCounter.text;
			activity.State = "In a speedrun.";
		}
		if(LM.levelID == 0)
		{
			activity.Details = inScene;
			activity.State = theTime;
		}

		activityManager.UpdateActivity(activity, (res) =>
		{
			if (res == Discord.Result.Ok)
			{
				
			}
		});
		
		discord.RunCallbacks();
	}

	public void Quit()
	{
		var activityManager = discord.GetActivityManager();
		
		activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Success!");
            }
            else
            {
                Debug.Log("Failed");
            }
});
	}

	public long UnixTimeNow()
    {
		var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalSeconds;

    }
}