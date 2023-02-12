using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GamePlayerUI : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] TextMeshProUGUI _playerOneScoreLabel;
    [SerializeField] TextMeshProUGUI _playerOneLivesLeftLabel;
    [SerializeField] GameObject _playerOneDurability;
    [SerializeField] GameObject _playerOneHealth;


    public int _playerOneScoreNum;
    public int _playerOneLivesLeftNum;
    public float _playerOneDurabilityScore = 0.2f;
    public float _playerOneHealthScore;


   
    private void Update()
    {

        // Only for testing movement of inputs need to call in correct parts of game
        if (Input.GetKeyDown("z"))
        {
            ChangePlayerOneScore(1);
        }

        if (Input.GetKeyDown("x"))
        {
            ChangePlayerOneDurability(0.4f);
        }

        if (Input.GetKeyDown("c"))
        {
            ChangePlayerOneHealth(0.4f);
        }

        if (Input.GetKeyDown("v"))
        {
            ChangePlayerOneLivesLeft(-1);
        }
    }


    public void ChangePlayerOneScore(int changeBy)
    {
        _playerOneScoreNum = _playerOneScoreNum + changeBy;
        _playerOneScoreLabel.text = _playerOneScoreNum.ToString();
    }

    public void ChangePlayerOneDurability(float changeBy)
    {
        _playerOneDurabilityScore = _playerOneDurabilityScore + changeBy;
        _playerOneDurability.GetComponent<Image>().fillAmount = _playerOneDurabilityScore;
    }

    public void ChangePlayerOneHealth(float changeBy)
    {
        _playerOneHealthScore = _playerOneHealthScore + changeBy;
        _playerOneHealth.GetComponent<Image>().fillAmount = _playerOneHealthScore;
    }

    public void ChangePlayerOneLivesLeft(int changeBy)
    {
        _playerOneLivesLeftNum = _playerOneLivesLeftNum + changeBy;
        _playerOneLivesLeftLabel.text = _playerOneLivesLeftNum.ToString();
    }

}
