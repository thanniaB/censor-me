using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreController : MonoBehaviour {
    public void RestartBuyings() {
        Stats myData = UserProfile.LoadData();
        myData.WorldsOwned = new List<World>();
        myData.WorldsOwned.Add(World.BASIC);
        myData.WorldSelected = World.BASIC;
        UserProfile.SaveData(myData);
        UnityEngine.SceneManagement.SceneManager.LoadScene("store");
    }
}
