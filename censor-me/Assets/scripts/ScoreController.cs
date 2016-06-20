using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    private GameController gameController;
    private Text scoreText;

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        scoreText = GetComponent<Text>();
    }
	
	void LateUpdate () {
        scoreText.text = gameController.Coins().ToString();
    }
}
