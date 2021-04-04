using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;
using System;

public class MenuDiscordController : MonoBehaviour
{
	public Discord.Discord discord;
	public long clientID;
	public string inScene;

	// Use this for initialization
	public void Start()
	{
		discord = new Discord.Discord(clientID, (System.UInt64)Discord.CreateFlags.Default);
	}

	// Update is called once per frame
	public void Update()
	{
		Scene scene = SceneManager.GetActiveScene();
		inScene = "In " + scene.name;

		var activityManager = discord.GetActivityManager();

		var activity = new Discord.Activity
		{
			Details = inScene,
			Assets =
			{
				LargeImage = "bean",
				LargeText = inScene,
			},
		};

		if(inScene == "MainMenu"){
			activity.Assets.LargeText = inScene;
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