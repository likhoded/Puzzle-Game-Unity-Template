using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml.Linq;

public class PuzzleManager : MonoBehaviour
{
    public MenuManager Menumanager;
    public GameManager GM;

    private float imageSizeX = 6f;
    private float imageSizeY = 6f;

    private Dictionary<string, Sprite> piecesMap = new Dictionary<string, Sprite>();
    public List<GameObject> PiecesObjects = new List<GameObject>();


    [Serializable]
    public struct NamedImage {
        public string name;
        public Sprite image; 
    }
    public NamedImage[] puzzleImages;

    public NamedImage[] planeImages;

    public GameObject initialPositionObject;
    public GameObject PuzzlePlace;
    public GameObject piecePrefab;
    public GameObject imagePrefab;
    public GameObject scrollView;

    public float _columnSpacing;
    public void ClearAll()
    {
        piecesMap.Clear();
        PiecesObjects.Clear();
    }

    #region PuzzleGenerator
    public void GeneratePuzzle(int nPieceCol, int nPieceRow, Sprite image)
	{
        Menumanager.SettingMenu.SetActive(false);
        Menumanager.Menu.SetActive(false);
        
        imageSizeX = initialPositionObject.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        imageSizeY = initialPositionObject.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        
        foreach (var namedImage in puzzleImages)
        {
            piecesMap.Add(namedImage.name, namedImage.image);
        }
        
        
        var pieceOffsetRatio = 0.75f;
        var pieceInitialSizeX = imageSizeX / nPieceCol;
        var pieceSizeX = pieceInitialSizeX / pieceOffsetRatio;
        var pieceInitialSizeY = imageSizeY / nPieceRow;
        var pieceSizeY = pieceInitialSizeY / pieceOffsetRatio;
        for (int c = 0; c < nPieceCol; c++)
        {
            string pieceName;
            float x;
            float y;

            for (int r = 0; r < nPieceRow; r++)
            {
                pieceName = "";
                
                if (r == 0)
                    pieceName += "1";
                else
                    pieceName += "0";

                if (c==nPieceCol-1)
                    pieceName += "1";
                else
                    pieceName += "2";

                if (r == nPieceRow - 1)
                    pieceName += "1";
                else
                    pieceName += "2";
                
                if (c==0)
                    pieceName += "1";
                else
                    pieceName += "0";
                
                if (c == 0)
                    x = 0;
                else
                    x = pieceInitialSizeX * c;
                if (c== nPieceCol - 1)
                    x -= pieceSizeX * 0.25f;
                
                if (r == 0)
                    y = 0;
                else
                    y = - pieceInitialSizeY * r;
                if (r == nPieceRow - 1)
                    y += pieceSizeY * 0.25f;
                
                var piece = Instantiate(piecePrefab, initialPositionObject.transform, true);
                piece.transform.localPosition = new Vector3(x, y, 0f);
                
                var collider = piece.GetComponent<BoxCollider2D>();
                collider.offset = new Vector2(
                    collider.offset.x + (c == nPieceCol - 1 ? pieceInitialSizeX * 0.25f : 0),
                    collider.offset.y - (r == nPieceRow - 1 ? pieceInitialSizeY * 0.25f : 0));
                
                piece.transform.localScale = new Vector3(pieceSizeX, pieceSizeY, 1);
                
                piece.GetComponentInChildren<Image>().sprite = piecesMap[pieceName];

                Menumanager.PuzzlePlaceObject.GetComponent<Image>().sprite = image;
                var imagePlaceholder = Instantiate(imagePrefab, initialPositionObject.transform);
                imagePlaceholder.GetComponentInChildren<Image>().sprite = image;
                imagePlaceholder.transform.SetParent(piece.transform.GetChild(0));

            }
        }
    }
    #endregion

    #region PlaneGenerator
    public void GeneratePlane(int nPieceCol, int nPieceRow, Sprite image)
    {
        Menumanager.SettingMenu.SetActive(false);
        Menumanager.Menu.SetActive(false);

        imageSizeX = initialPositionObject.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        imageSizeY = initialPositionObject.transform.GetChild(0).GetComponent<RectTransform>().rect.height;

        foreach (var namedImage in planeImages)
        {
            piecesMap.Add(namedImage.name, namedImage.image);
        }


        var pieceOffsetRatio = 0.75f;
        var pieceInitialSizeX = imageSizeX / nPieceCol;
        var pieceSizeX = pieceInitialSizeX / pieceOffsetRatio;
        var pieceInitialSizeY = imageSizeY / nPieceRow;
        var pieceSizeY = pieceInitialSizeY / pieceOffsetRatio;
        for (int c = 0; c < nPieceCol; c++)
        {
            string pieceName;
            float x;
            float y;

            for (int r = 0; r < nPieceRow; r++)
            {
                pieceName = "";

                if (r == 0)
                    pieceName += "1";
                else
                    pieceName += "0";

                if (c == nPieceCol - 1)
                    pieceName += "1";
                else
                    pieceName += "2";

                if (r == nPieceRow - 1)
                    pieceName += "1";
                else
                    pieceName += "2";

                if (c == 0)
                    pieceName += "1";
                else
                    pieceName += "0";

                if (c == 0)
                    x = 0;
                else
                    x = pieceInitialSizeX * c;
                if (c == nPieceCol - 1)
                    x -= pieceSizeX * 0.25f;

                if (r == 0)
                    y = 0;
                else
                    y = -pieceInitialSizeY * r;
                if (r == nPieceRow - 1)
                    y += pieceSizeY * 0.25f;

                var piece = Instantiate(piecePrefab, initialPositionObject.transform, true);
                piece.transform.localPosition = new Vector3(x, y, 0f);

                var collider = piece.GetComponent<BoxCollider2D>();
                collider.offset = new Vector2(
                    collider.offset.x + (c == nPieceCol - 1 ? pieceInitialSizeX * 0.25f : 0),
                    collider.offset.y - (r == nPieceRow - 1 ? pieceInitialSizeY * 0.25f : 0));

                piece.transform.localScale = new Vector3(pieceSizeX, pieceSizeY, 1);

                piece.GetComponentInChildren<Image>().sprite = piecesMap[pieceName];

                Menumanager.PuzzlePlaceObject.GetComponent<Image>().sprite = image;
                var imagePlaceholder = Instantiate(imagePrefab, initialPositionObject.transform);
                imagePlaceholder.GetComponentInChildren<Image>().sprite = image;
                imagePlaceholder.transform.SetParent(piece.transform.GetChild(0));

            }
        }
    }
    #endregion

    public void CheckPosition()
    {
        int count = 0;
        for(int i = 0;i< PiecesObjects.Count;i++)
        {
            if(PiecesObjects[i].GetComponent<Respawn>().CheckPositionThis() == true)
            {
                count++;
            }
        }

        if(PiecesObjects.Count == count)
        {
            Menumanager.NewGame();
            Debug.Log("œ¿«À —Œ¡–¿Õ");
        }
    }
}

