using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 initialPosition;

    private float timeLeft = 2.0f;

    PuzzleManager puzzleManager;
    MenuManager menuManager;

    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        menuManager = FindObjectOfType<MenuManager>();

        initialPosition = transform.position;

        puzzleManager.PiecesObjects.Add(transform.gameObject);
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && timeLeft > -1)
        {
            transform.SetParent(menuManager.LayoutGr.transform);
            transform.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void ReturnToInitialPosition()
    {
        transform.position = initialPosition;

        var _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }
    
    public bool CheckPositionThis()
    {
        if (transform.position == initialPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetInitialPosition()
    {
        return initialPosition;
    }

    public void SetInitialPosition()
    {
        initialPosition = transform.position;
    }
}
