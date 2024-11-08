using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameStatistics gameStatistics;

    #region Initialization

    private void Awake()
    {
        EventManager.OnStartGameSelected += StartGame;
    }

    private void OnDestroy()
    {
        EventManager.OnStartGameSelected -= StartGame;

        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= ExitGame;
        EventManager.OnStatisticsLoaded += OnStatisticsLoaded;
        EventManager.OnCharacterMoved -= CharacterMove;
        EventManager.OnCharacterJumped -= CharacterJump;
        EventManager.OnCharacterTrampolined -= OnCharacterTrampoline;
        EventManager.OnCharacterHitten -= CharacterHit;
        EventManager.OnGameFinished -= FinishGame;
    }

    #endregion Initialization

    #region Private Helpers
    private void StartGame()
    {
        EventManager.OnPauseGameSelected += PauseGame;
        EventManager.OnResumeGameSelected += ResumeGame;
        EventManager.OnMainMenuSelected += ExitGame;
        EventManager.OnStatisticsLoaded += OnStatisticsLoaded;
        EventManager.OnCharacterMoved += CharacterMove;
        EventManager.OnCharacterJumped += CharacterJump;
        EventManager.OnCharacterTrampolined += OnCharacterTrampoline;
        EventManager.OnCharacterHitten += CharacterHit;
        EventManager.OnGameFinished += FinishGame;

        gameStatistics = new GameStatistics();
        EventManager.OnStatisticsToLoad();
        EventManager.OnLifesChange(gameStatistics.currentLifes);
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
        EventManager.OnStatisticsResult(gameStatistics);
    }
    private void ExitGame()
    {
        EventManager.OnPauseGameSelected -= PauseGame;
        EventManager.OnResumeGameSelected -= ResumeGame;
        EventManager.OnMainMenuSelected -= ExitGame;
        EventManager.OnStatisticsLoaded += OnStatisticsLoaded;
        EventManager.OnCharacterMoved -= CharacterMove;
        EventManager.OnCharacterJumped -= CharacterJump;
        EventManager.OnCharacterTrampolined -= OnCharacterTrampoline;
        EventManager.OnCharacterHitten -= CharacterHit;
        EventManager.OnGameFinished -= FinishGame;

        EventManager.OnStatisticsSave(gameStatistics);
    }

    private void CharacterHit()
    {
        gameStatistics.IncrementHits();
        gameStatistics.DecrementCurrentLifes();

        EventManager.OnLifesChange(gameStatistics.currentLifes);
    }

    private void CharacterJump()
    {
        gameStatistics.IncrementJumps();
    }

    private void OnCharacterTrampoline()
    {
        gameStatistics.IncrementMushrooms();
    }

    private void CharacterMove(float speed)
    {
        gameStatistics.IncrementScore(speed/100);

        EventManager.OnScoreChange((int)gameStatistics.currentScore);
    }

    #endregion Private Helpers
}
