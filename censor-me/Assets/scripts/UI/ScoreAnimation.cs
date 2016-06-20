using UnityEngine;
using UnityEngine.UI;

public class ScoreAnimation : Timer {
    [SerializeField]
    private GameObject scoreCanvas;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private GameObject globalScoreCanvas;
    [SerializeField]
    private Text globalScoreText;
    [SerializeField]
    private GameObject highScoreLabel;
    [SerializeField]
    private GameController gameController;

    private AudioManager audioManager;
    private int sessionScore;
    private int globalScore;
    private bool showNewHighLabel;

    // steps
    private AnimationStep currentStep;

    // Use this for initialization
    public override void Start () {
        // disable
        scoreCanvas.SetActive(false);
        globalScoreCanvas.SetActive(false);

        // init
        audioManager = AudioManager.Instance;

        timeOut = 99f;
    }

    public void PlayAnimation() {
        gameObject.SetActive(true);
        sessionScore = gameController.Coins();

        // calculations
        Stats myData = UserProfile.LoadData();
        globalScore = myData.GlobalScore;
        int bestScore = myData.BestScore;
        showNewHighLabel = sessionScore > bestScore;

        // save
        myData.GlobalScore += sessionScore;
        if (showNewHighLabel) {
            myData.BestScore = sessionScore;
        }
        UserProfile.SaveData(myData);

        timeOut = 0.5f;
        base.Start();
    }

    public override void Callback() {
        switch (currentStep) {
            case AnimationStep.STEP1:
                audioManager.PlayMenuSFX();
                scoreCanvas.SetActive(true);
                currentStep = showNewHighLabel ? AnimationStep.STEP2 : AnimationStep.STEP3;
                timeOut = 0.5f;
                break;
            case AnimationStep.STEP2:
                highScoreLabel.SetActive(true);
                audioManager.PlayMenuSFX();
                currentStep = AnimationStep.STEP3;
                break;
            case AnimationStep.STEP3:
                audioManager.PlayMenuSFX();
                globalScoreCanvas.SetActive(true);
                currentStep = AnimationStep.STEP4;
                timeOut = 0.5f;
                break;
            case AnimationStep.STEP4:
                timeOut = 0.01f;
                if (sessionScore > 0) {
                    AudioManager.Instance.PlayCoinSFX();
                    sessionScore = sessionScore - 1;
                    globalScore = globalScore + 1;
                } else {
                    currentStep = AnimationStep.STEP5;
                }
                break;
            default:
                highScoreLabel.SetActive(false);
                audioManager.PlayMenuSFX();
                timeOut = 99;
                break;
        }
    }

    void LateUpdate() {
        UpdateTextScoreAndHigh();
    }

    private void UpdateTextScoreAndHigh() {
        scoreText.text = sessionScore.ToString();
        globalScoreText.text = globalScore.ToString();
    }

    private void SetActiveRecursively(GameObject rootObject, bool active) {
        rootObject.SetActive(active);

        foreach (Transform childTransform in rootObject.transform) {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }

    enum AnimationStep {
        STEP1, STEP2, STEP3, STEP4, STEP5
    }
}
