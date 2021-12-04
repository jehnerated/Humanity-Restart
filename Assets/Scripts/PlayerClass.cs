
public static class Player
{
    public static int playerResources = 0;

    public static string PlayerResources()
    {
        return playerResources.ToString("C0");
    }

    public static void subtractResource(int amount)
    {
        playerResources -= amount;
    }
    public static void addResource(int amount)
    {
        playerResources += amount;
    }
}
