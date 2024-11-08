public class GameStatistics
{
    public float currentScore = 0;
    public int currentMushrooms = 0;
    public int currentJumps = 0;
    public int currentHits = 0;
    public int currentObstacles = 0;
    public int currentLifes = 3;

    public float totalScore = 0;
    public int totalMushrooms = 0;
    public int totalJumps = 0;
    public int totalHits = 0;
    public int totalObstacles = 0;
    public int totalLifesUsed = 0;

    public void IncrementMushrooms()
    {
        currentMushrooms++;
    }

    public void IncrementJumps()
    {
        currentJumps++;
    }

    public void IncrementHits()
    {
        currentHits++;
    }

    //public void IncrementObstacles()
    //{
    //    obstacles++;
    //}

    public void IncrementScore(float increment)
    {
        currentScore += increment;
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