using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text generationText;

    // Play button
    [SerializeField]
    private Button playButton;

    // Step button
    [SerializeField]
    private Button stepButton;

    [SerializeField]
    private Button restartButton;

    // 2D Game
    [SerializeField]
    private GameOfLife game2D;

    // 3D Game
    [SerializeField]
    private GameOfLife game3D;

    // The amount of time a single game tick takes in seconds
    [SerializeField]
    private Slider tickInterval;

    private GameOfLife game;
    private bool modeIs2d;

    private void Awake() {
        modeIs2d = true;
        game = game2D;
        game2D.gameObject.SetActive(true);
        game3D.gameObject.SetActive(false);
        game.SetTickInterval(tickInterval.value);
        GameOfLife.GenerationChanged += SetGenerationText;
        GameOfLife.PauseStateChanged += SetPauseButton;
    }

    public void SetTickInterval(float interval) {
        game.SetTickInterval(interval);
    }

    private void SetGenerationText(int generation) {
        generationText.text = "Generation: " + generation; 
    }

    private void SetPauseButton(bool paused) {
        playButton.GetComponentInChildren<Text>().text = paused ? "Play" : "Pause";
    }

    public void PlayPause() {
        game.TogglePause();
    }

    public void Step() {
        game.Tick();
    }

    public void ResetGame() {
        game.Restart();
    }

    public void ChangeGameMode() {
        modeIs2d = !modeIs2d;
        game.Pause();
        SetPauseButton(true);
        game.gameObject.SetActive(false);
        if (modeIs2d) {
            game = game2D;
        } else {
            game = game3D;
        }
        game.gameObject.SetActive(true);
        SetGenerationText(game.GetGeneration());
        game.SetTickInterval(tickInterval.value);
    }

    public void Quit() {
        SceneManager.LoadScene("Main Menu");
    }
}
