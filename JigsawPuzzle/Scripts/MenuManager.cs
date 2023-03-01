using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public GameManager GameManager;
	public PuzzleManager PuzzleManager;

    public GameObject Puzzle;
	public GameObject GameMenu;
    public GameObject PuzzlePlaceObject;

    public List<Button> ListButton = new List<Button>();
	public List<Button> ListSizeButton = new List<Button>();

	public GameObject Menu;
    public GameObject SettingMenu;
	public GameObject NewGameMenu;

    public Image SettingMenuImage;
	public Image Background;

	public Slider SliderObj;

	public Sprite PuzzleSprite;
	public Sprite PlaneSprite;

    public GameObject SliderSprite;

    public Button NewGame_01;
	public Button NewGame_02;

	public LayoutGroup LayoutGr;

	private int _selectedImage = -1;
    void Start()
    {
        for(int i = 0; i < GameManager.PuzzleSprites.Count; i++)
        {
            ListButton[i].image.sprite = GameManager.PuzzleSprites[i];
        }
    }

    public void SelectImage(int num)
    {
		OpenCloseSettingMenu();

        SettingMenuImage.sprite = GameManager.PuzzleSprites[num];
		_selectedImage = num;
		NewGameMenu.SetActive(false);
    }
	
	public void SelectNewGame(int num)
	{
        GameObject[] puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            GameObject.Destroy(puzzlePiece);
        }
        PuzzleManager.ClearAll();
        NewGameMenu.SetActive(false);

        PuzzleManager.GeneratePuzzle(3, 3, GameManager.PuzzleSprites[num]);
    }
	public void SelectSize(int num)
	{
		switch(num)
		{
			case 9:
			{
				Puzzle.SetActive(true);
				GameMenu.SetActive(true);
					if (SliderObj.value == 1)
					{
						PuzzleManager.GeneratePuzzle(3, 3, GameManager.PuzzleSprites[_selectedImage]);
					}
					else
					{
                        PuzzleManager.GeneratePlane(3, 3, GameManager.PuzzleSprites[_selectedImage]);
                    }
                    break;
			}
			case 16:
			{
				Puzzle.SetActive(true);
				GameMenu.SetActive(true);
					if (SliderObj.value == 1)
					{
						PuzzleManager.GeneratePuzzle(4, 4, GameManager.PuzzleSprites[_selectedImage]);
					}
					else
					{
                        PuzzleManager.GeneratePlane(4, 4, GameManager.PuzzleSprites[_selectedImage]);
                    }
				break;
			}
			case 36:
			{
				Puzzle.SetActive(true);
				GameMenu.SetActive(true);
					if (SliderObj.value == 1)
					{
						PuzzleManager.GeneratePuzzle(6, 6, GameManager.PuzzleSprites[_selectedImage]);
					}
					else
					{
                        PuzzleManager.GeneratePlane(6, 6, GameManager.PuzzleSprites[_selectedImage]);
                    }
				break;
			}
			case 64:
			{
				Puzzle.SetActive(true);
				GameMenu.SetActive(true);
					if (SliderObj.value == 1)
					{
						PuzzleManager.GeneratePuzzle(8, 8, GameManager.PuzzleSprites[_selectedImage]);
					}
					else
					{
                        PuzzleManager.GeneratePlane(8, 8, GameManager.PuzzleSprites[_selectedImage]);
                    }
				break;
			}
			case 100:
			{
				Puzzle.SetActive(true);
                GameMenu.SetActive(true);
					if (SliderObj.value == 1)
					{
						PuzzleManager.GeneratePuzzle(10, 10, GameManager.PuzzleSprites[_selectedImage]);
					}
					else
					{
                        PuzzleManager.GeneratePlane(10, 10, GameManager.PuzzleSprites[_selectedImage]);
                    }
                break;
			}
		}
	}

	public void OpenCloseSettingMenu()
	{
		if(SettingMenu.activeSelf == true)
		{
			SettingMenu.SetActive(false);
        }
		else
		{
			SettingMenu.SetActive(true);
            if (SliderObj.value == 0)
            {
                SliderSprite.GetComponent<Image>().sprite = PlaneSprite;
                for (int i = 0; i < ListSizeButton.Count; i++)
                {
                    ListSizeButton[i].image.sprite = PlaneSprite;
                }
            }
            else if (SliderObj.value == 1)
            {
                SliderSprite.GetComponent<Image>().sprite = PuzzleSprite;
                for (int i = 0; i < ListSizeButton.Count; i++)
                {
                    ListSizeButton[i].image.sprite = PuzzleSprite;
                }
            }
        }
	}

	public void BackButton()
	{
		GameMenu.SetActive(false);
        SettingMenu.SetActive(false);
        Menu.SetActive(true);
        GameObject[] puzzlePieces = GameObject.FindGameObjectsWithTag("PuzzlePiece");
        foreach (GameObject puzzlePiece in puzzlePieces)
        {
            GameObject.Destroy(puzzlePiece);
        }
        PuzzleManager.ClearAll();
        NewGameMenu.SetActive(false);
		Puzzle.SetActive(false);
    }	

	public void PuzzlePlaceButton()
	{
		if(PuzzlePlaceObject.activeSelf == false)
		{
			PuzzlePlaceObject.SetActive(true);
		}
		else
		{
			PuzzlePlaceObject.SetActive(false);
		}
	}

	public void ColorButton()
	{
		int colorNum = 0;
		switch(colorNum)
		{
			case 0:
				{
					Background.color = Color.green;
					colorNum = 1;
					break;
				}
			case 1:
				{
                    Background.color = Color.blue;
                    colorNum = 2;
                    break;
                }
			case 2:
				{
                    Background.color = Color.yellow;
                    colorNum = 0;
                    break;
                }

		}
	}

	public void NewGame()
	{
		NewGameMenu.SetActive(true);
        NewGame_01.image.sprite = GameManager.PuzzleSprites[4];
        NewGame_02.image.sprite = GameManager.PuzzleSprites[3];
    }
	public void TypeSliderChangeState()
	{
        if (SliderObj.value == 0)
        {
            SliderSprite.GetComponent<Image>().sprite = PlaneSprite;
            for (int i = 0; i < ListSizeButton.Count; i++)
            {
                ListSizeButton[i].image.sprite = PlaneSprite;
            }
        }
        else if (SliderObj.value == 1)
        {
            SliderSprite.GetComponent<Image>().sprite = PuzzleSprite;
            for (int i = 0; i < ListSizeButton.Count; i++)
            {
                ListSizeButton[i].image.sprite = PuzzleSprite;
            }
        }
    }
    public void InitialButton()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(PuzzleManager.PiecesObjects.Count);
		GameObject piece = PuzzleManager.PiecesObjects[index];
        piece.GetComponent<Respawn>().ReturnToInitialPosition();
		piece.transform.SetParent(PuzzleManager.initialPositionObject.transform);
		PuzzleManager.CheckPosition();
    }
}
