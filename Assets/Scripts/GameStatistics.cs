public class GameStatistics
{
    public float score = 0;
    public int mushrooms = 0;
    public int jumps = 0;
    public int hits = 0;
    public int obstacles = 0;
    public int currentLifes = 3;
    public int totalLifesUsed = 0;

    public void IncrementMushrooms()
    {
        mushrooms++;
    }

    public void IncrementJumps()
    {
        jumps++;
    }

    public void IncrementHits()
    {
        hits++;
    }

    public void IncrementObstacles()
    {
        obstacles++;
    }

    public void IncrementScore(float increment)
    {
        score += increment;
    }

    public void IncrementLifesUsed()
    {
        totalLifesUsed++;
    }

    public void DecrementCurrentLifes()
    {
        currentLifes--;
        if (currentLifes <= 0)
        {
            EventManager.OnGameFinish();
        }
    }
}