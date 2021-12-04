using System.Collections.Generic;
using UnityEngine;

public static class StatsManager
{
    private static List<GameObject> playerUnits = new List<GameObject>();
    private static List<GameObject> playerLostUnits = new List<GameObject>();
    private static List<GameObject> playerBuildings = new List<GameObject>();
    private static List<GameObject> playerLostBuildings = new List<GameObject>();
    private static int playerMinedResources;
    private static int playerSpentResources;

    private static List<GameObject> enemyUnits = new List<GameObject>();
    private static List<GameObject> enemyLostUnits = new List<GameObject>();
    private static List<GameObject> enemyBuildings = new List<GameObject>();
    private static List<GameObject> enemyLostBuildings = new List<GameObject>();
    private static int enemyMinedResources;
    private static int enemySpentResources;

    public static void AddPlayerUnit(ref GameObject unit)
    {
        playerUnits.Add(unit);
    }

    public static void PlayerUnitLost(GameObject unit)
    {
        try
        {
            playerUnits.Remove(unit);
            playerUnits.Add(unit);
        }
        catch
        {
            Debug.Log("Lost Unit Not Found");
        }
    }

    public static void AddPlayerBuilding(GameObject building)
    {
        playerBuildings.Add(building);
    }

    public static void PlayerBuildingLost(GameObject building)
    {
        try
        {
            playerBuildings.Remove(building);
            playerUnits.Add(building);
        }
        catch
        {
            Debug.Log("Lost Unit Not Found");
        }
    }

    public static void AddEnemyUnit(GameObject unit)
    {
        enemyUnits.Add(unit);
    }

    public static void EnemyUnitLost(GameObject unit)
    {
        try
        {
            enemyUnits.Remove(unit);
            playerUnits.Add(unit);
        }
        catch
        {
            Debug.Log("Lost Unit Not Found");
        }
    }

    public static void AddEnemyBuilding(GameObject building)
    {
        enemyBuildings.Add(building);
    }

    public static void EnemyBuildingLost(GameObject building)
    {
        try
        {
            enemyBuildings.Remove(building);
            playerUnits.Add(building);
        }
        catch
        {
            Debug.Log("Lost Unit Not Found");
        }
    }
}
