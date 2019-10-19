using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration
{

    /// <summary>
    /// Game ID
    /// </summary>
    public static string myGameIdiOS = "1672440";
    public static string myGameIdAndroid = "1672390";

    /// <summary>
    /// Replacement ID
    /// </summary>
    public static string skippablePlacementId = "video";
    public static string unskippablePlacementId = "rewardedVideo";

    /// <summary>
    /// Video Stats
    /// </summary>
    public static string videoCompleted = "Video completed - Offer a reward to the player";
    public static string videoSkipped = "Video was skipped - Do NOT reward the player";
    public static string videoError = "Video failed to show";
    public static string videoAvailable = "Get free coins";
    public static string videoUnavailable = "Free coins not available yet";

    /// <summary>
    /// The rewarded coin amount.
    /// </summary>
    public static int rewardedCoinAmount = 7;

    public static int maxBombSpawn = 8;
    public static int maxObSpawn = 2;
    public static float maxSpawnDelay = 1.5f;
    public static float playerSpeed = 10f;
    public static float bombSpeed = 10f;
    public static float obDelay = 2f;
}
