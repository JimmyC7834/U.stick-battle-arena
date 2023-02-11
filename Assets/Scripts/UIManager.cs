using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update


 
    public TextMeshProUGUI playerOneScoreLabel;
    public TextMeshProUGUI playerOneLivesLeftLabel;
    public GameObject playerOneDurability;
    public GameObject playerOneHealth;
    public GameObject playerOneInventory;


    public int playerOneScoreNum;
    public int playerOneLivesLeftNum;
    public float playerOneDurabilityScore = 0.2f;
    public float playerOneHealthScore;
    public string playerOneInventoryType;

    //public GameObject playerTwoScore;
    //public GameObject playerTwoDurability;
    //public GameObject playerTwoHealth;
    //public GameObject playerTwoInventory;
    //public int playerTwoScoreNum;
    //public int playerTwoDurabilityScore;
    //public int playerTwoHealthScore;
    //public string playerTwoInventoryType;



    private void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            changePlayerOneScore(1);
            Debug.Log("click");
        }

        if (Input.GetKeyDown("x"))
        {
            changePlayerOneDurability(0.4f);
            Debug.Log("click");
        }

        if (Input.GetKeyDown("c"))
        {
            changePlayerOneHealth(0.4f);
            Debug.Log("click");
        }

        if (Input.GetKeyDown("v"))
        {
            changePlayerOneLivesLeft(-1);
        }

    }


    public void changePlayerOneScore(int changeBy)
    {
        playerOneScoreNum = playerOneScoreNum + changeBy;
        playerOneScoreLabel.text = playerOneScoreNum.ToString();
        //Image buttonImage = newTextureButton.gameObject.GetComponent<Image>();
    }

    public void changePlayerOneDurability(float changeBy)
    {
        playerOneDurabilityScore = playerOneDurabilityScore + changeBy;
        playerOneDurability.GetComponent<Image>().fillAmount = playerOneDurabilityScore;
        //change player
    }

    public void changePlayerOneHealth(float changeBy)
    {
        playerOneHealthScore = playerOneHealthScore + changeBy;
        playerOneHealth.GetComponent<Image>().fillAmount = playerOneHealthScore;
        //change player
    }

    public void changePlayerOneLivesLeft(int changeBy)
    {
        playerOneLivesLeftNum = playerOneLivesLeftNum + changeBy;
        playerOneLivesLeftLabel.text = playerOneLivesLeftNum.ToString();
    }










    //public void changePlayerTwoScore(int changeBy)
    //{
    //    playerTwoScoreNum = playerTwoScoreNum + changeBy;
    //}


    //public void changePlayerTwoHealth(int changeBy)
    //{
    //    playerTwoHealthScore = playerTwoHealthScore + changeBy;

    //    //change playerOneHealthBar

    //}

    //public void changePlayerTwoDurability(int changeBy)
    //{
    //    playerTwoDurabilityScore = playerTwoDurabilityScore + changeBy;

    //    //change player
    //}


    public void changePlayerOneWeaponType(string weaponType)
    {
        playerOneInventoryType = weaponType;

        //change player one weapon image
    }

    //public void changePlayerTwoWeaponType(string weaponType)
    //{
    //    playerTwoInventoryType = weaponType;
    //}




}
