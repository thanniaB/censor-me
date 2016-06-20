using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalScoreController : MonoBehaviour {
    [SerializeField]
    private Text scoreText;

    void LateUpdate () {
        scoreText.text = UserProfile.LoadData().GlobalScore.ToString();
    }
}
