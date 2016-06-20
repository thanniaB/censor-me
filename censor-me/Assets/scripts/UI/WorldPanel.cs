using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WorldPanel : MonoBehaviour {
    [SerializeField]
    private World world;
    [SerializeField]
    private int price;
    [SerializeField]
    private StoreController storeController;
    [SerializeField]
    private GameObject buyButton;

    private Stats myData;

    void LateUpdate() {
        LoadData();

        Image myImage = GetComponent<Image>();

        if (myData.WorldSelected == world) {
            myImage.color = new Color(1f, 0f, 0f, 0.5f);
        } else {
            myImage.color = new Color(1f, 1f, 1f, 0.5f);
        }

        if(IsWorldOwned()) {
            buyButton.SetActive(false);
        }
    }

    public void Buy() {
        if (!IsWorldOwned() && myData.GlobalScore >= price) {
            myData.WorldsOwned.Add(world);
            myData.WorldSelected = world;
            myData.GlobalScore -= price;
            SaveData();
        }
    }

    public void Select() {
        if (IsWorldOwned()) {
            myData.WorldSelected = world;
            SaveData();
        }
    }

    private bool IsWorldOwned() {
        return myData.WorldsOwned.Contains(world);
    }

    private void LoadData() {
        myData = UserProfile.LoadData();
    }

    private void SaveData() {
        UserProfile.SaveData(myData);
        LoadData();
    }
}
