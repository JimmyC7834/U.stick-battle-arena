using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI playerOneScoreLabel;
    public GameObject playerOneDurability;
    public GameObject playerOneHealth;
    public GameObject playerOneInventory;
    public int playerOneScoreNum;
    public int playerOneDurabilityScore;
    public int playerOneHealthScore;
    public string playerOneInventoryType;

    public GameObject playerTwoScore;
    public GameObject playerTwoDurability;
    public GameObject playerTwoHealth;
    public GameObject playerTwoInventory;
    public int playerTwoScoreNum;
    public int playerTwoDurabilityScore;
    public int playerTwoHealthScore;
    public string playerTwoInventoryType;



    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            changePlayerOneScore(1);
            Debug.Log("click");
        }
        
    }


    public void changePlayerOneScore(int changeBy)
    {
        playerOneScoreNum = playerOneScoreNum + changeBy;
        playerOneScoreLabel.text = playerOneScoreNum.ToString();
    }

    public void changePlayerTwoScore(int changeBy)
    {
        playerTwoScoreNum = playerTwoScoreNum + changeBy;
    }

    public void changePlayerOneHealth(int changeBy)
    {
        playerOneHealthScore = playerOneHealthScore + changeBy;

        //change playerOneHealthBar

    }


    public void changePlayerTwoHealth(int changeBy)
    {
        playerTwoHealthScore = playerTwoHealthScore + changeBy;

        //change playerOneHealthBar

    }

    public void changePlayerOneDurability(int changeBy)
    {
        playerOneDurabilityScore = playerOneDurabilityScore + changeBy; 

        //change player
    }

    public void changePlayerTwoDurability(int changeBy)
    {
        playerTwoDurabilityScore = playerTwoDurabilityScore + changeBy;

        //change player
    }


    public void changePlayerOneWeaponType(string weaponType)
    {
        playerOneInventoryType = weaponType;

        //change player one weapon image
    }

    public void changePlayerTwoWeaponType(string weaponType)
    {
        playerTwoInventoryType = weaponType;
    }




}
