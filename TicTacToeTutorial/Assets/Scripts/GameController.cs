using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
	//Declare values in inspector by dragging from hierarchy
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor
{
	//Declare values in inspector by dragging from hierarchy
	public Color panelColor;
	public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
	private string playerSide;

	public GameObject gameOverPanel;
	public Text gameOvertext;

	private int movecount;

	public GameObject restartButton;

	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;

	private void Awake()
	{
		gameOverPanel.SetActive(false);
		SetGameContollerReferanceOnButtons();
		//playerSide = "X";
		movecount = 0;
		restartButton.SetActive(false);
		//SetPlayerColors(playerX, playerO);
	}

	void SetGameContollerReferanceOnButtons()
    { 
        for (int i =0; i <buttonList.Length; i++)
		{
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReferance(this);
		}
    }

	public void SetStartingSide(string startingSide)
	{
		playerSide = startingSide;
		if(playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		}
		else
		{
			SetPlayerColors(playerO, playerX);
		}

		StartGame();
	}

	void StartGame()
	{
		SetBoardInteractable(true);
		SetPlayerButtons(false);
		startInfo.SetActive(false);
	}

	public string GetPlayerSide()
	{
		return playerSide;
	}

	public void EndTurn()
	{
		movecount++;
		
		//check top row
		if(buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check middle row
		else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check bottom row
		else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
		{
			GameOver(playerSide);
		}

		//check top col
		else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check middle row
		else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check bottom row
		else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
		{
			GameOver(playerSide);
		}

		//check topleft to bottomright
		else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check bottomleft to topright
		else if (buttonList[6].text == playerSide && buttonList[4].text == playerSide && buttonList[2].text == playerSide)
		{
			GameOver(playerSide);
		}
		//check draw
		else if(movecount >= 9)
		{
			GameOver("draw");

			//maybe add something here to set both players to not active color???
		}
		//Debug.Log("EndTurn is not implemented!");
		else
		{
			ChangeSides();
		}
	}

	void SetPlayerColors(Player newPlayer, Player oldPlayer)
	{
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	void GameOver(string winningPlayer)
	{
		SetBoardInteractable(false);

		//SetGameOverText(playerSide + " Wins!");
		if (winningPlayer == "draw")
		{
			SetGameOverText("Its a Draw!");
			SetPlayerColorsInactive();
		}
		else
		{
			SetGameOverText(winningPlayer + " Wins!");
		}

		restartButton.SetActive(true);
	}

	void ChangeSides()
	{
		playerSide = (playerSide == "X") ? "O" : "X";
		if (playerSide == "X")
		{
			SetPlayerColors(playerX, playerO);
		}
		else
		{
			SetPlayerColors(playerO, playerX);
		}
	}

	void SetGameOverText(string value)
	{
		gameOverPanel.SetActive(true);
		gameOvertext.text = value;
	}

	public void RestartGame()
	{
		//playerSide = "X";
		movecount = 0;
		gameOverPanel.SetActive(false);
		restartButton.SetActive(false);
		SetPlayerButtons(true);
		SetPlayerColorsInactive();
		startInfo.SetActive(true);
		

		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].text = "";
		}

		//SetPlayerColors(playerX, playerO);

		
	}

	void SetBoardInteractable(bool toggle)
	{
		for (int i = 0; i < buttonList.Length; i++)
		{
			buttonList[i].GetComponentInParent<Button>().interactable = toggle;
		}
	}

	void SetPlayerButtons(bool toggle)
	{
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;
	}

	void SetPlayerColorsInactive()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;

		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;

	}
}
