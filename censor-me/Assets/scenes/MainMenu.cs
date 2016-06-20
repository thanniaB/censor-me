using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private bool autoPlayMusic;
    private AudioManager audioManager;

    void Start() {
        audioManager = AudioManager.Instance;
        if (autoPlayMusic) {
            audioManager.PlayMenuMusic();
        }
    }

    public void MenuClickSFX() {
        audioManager.PlayMenuSFX();
        audioManager.ToggleMute();
    }

    public void GoTo(string sceneName) {
        audioManager.PlayMenuSFX();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void GoToPlay() {
        audioManager.PlayMenuSFX();
        string nextWorld = "play_basic";

        switch(UserProfile.LoadData().WorldSelected) {
            case World.BIRDS:
                nextWorld = "play_birds";
                break;
            case World.MONSTERS:
                nextWorld = "play_monsters";
                break;
            case World.ZOMBIES:
                nextWorld = "play_zombies";
                break;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(nextWorld);
    }
}
