using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameStatistics gameStatistics;

    #region Initialization

    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += ExitGame;
        EventManager.OnStatisticsLoaded += OnStatisticsLoaded;
        EventManager.OnCharacterMoved += CharacterMove;
        EventManager.OnCharacterHitten += CharacterHitten;
        EventManager.OnGameFinished += FinishGame;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= ExitGame;
        EventManager.OnStatisticsLoaded += OnStatisticsLoaded;
        EventManager.OnCharacterMoved -= CharacterMove;
        EventManager.OnCharacterHitten -= CharacterHitten;
        EventManager.OnGameFinished -= FinishGame;
    }

    #endregion Initialization

    #region Private Helpers
    private void StartGame()
    {
        gameStatistics = new GameStatistics();
        EventManager.OnStatisticsToLoad();
    }

    private void OnStatisticsLoaded(GameStatistics _gameStatistics)
    {
        gameStatistics = _gameStatistics;
    }

    private void PauseGame()
    {

    }

    private void ResumeGame()
    {

    }
    private void FinishGame()
    {

    }
    private void ExitGame()
    {
        EventManager.OnStatisticsSave(gameStatistics);
    }

    private void CharacterHitten()
    {
        gameStatistics.IncrementHits();
        gameStatistics.DecrementCurrentLifes();

        EventManager.OnLifesChange(gameStatistics.currentLifes);
    }

    private void CharacterMove(float speed)
    {
        gameStatistics.IncrementScore(speed);
    }

    #endregion Private Helpers
}
