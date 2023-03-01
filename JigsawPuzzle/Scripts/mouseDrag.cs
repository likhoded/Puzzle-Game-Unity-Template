using UnityEngine;
using UnityEngine.UIElements;

public class mouseDrag : MonoBehaviour
{
    static int topOrder = 1;
    private Vector2 positionOnDragStartObject;
    private Vector2 positionOnDragStartCursor;
    private PuzzleManager puzzleManager;
    private MenuManager menuManager;
    private Respawn respawn;

    void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        menuManager = FindObjectOfType<MenuManager>();
        respawn = GetComponent<Respawn>();

        
    }
    public void OnMouseDrag()
    {
        if (Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height && Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width)
        {
            if (transform.parent == puzzleManager.scrollView.transform && transform.position.x > 4.93)
            {
                transform.SetParent(puzzleManager.initialPositionObject.transform);
            }
            
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
            var mouseMovePath = mousePosition - positionOnDragStartCursor;
            transform.position = positionOnDragStartObject + mouseMovePath;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Top")
        {
            Debug.Log("Top");
            transform.Translate(0, -0.05F, 0);
        }
        if (other.gameObject.tag == "Bottom")
        {
            Debug.Log("Bottom");
            transform.Translate(0, +0.05F, 0);
        }
        if (other.gameObject.tag == "Left")
        {
            Debug.Log("Left");
            transform.Translate(+0.05F, 0, 0);
        }
        if (other.gameObject.tag == "Right")
        {
            Debug.Log("Right");
            transform.position += new Vector3(-0.05F, 0, 0);
        }
    }

    private void OnMouseDown()
    {
        positionOnDragStartCursor = Camera.main.ScreenToWorldPoint(
        new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        positionOnDragStartObject = transform.position;
        GetComponent<Canvas>().sortingOrder = ++topOrder;
    }

    private void OnMouseUp()
    {
        if (isCloseToInitialPosition())
        {
            respawn.ReturnToInitialPosition();
        }
        if (transform.parent == puzzleManager.initialPositionObject.transform && transform.position.x > 4.93)
        {
            transform.SetParent(puzzleManager.scrollView.transform);
        }
        puzzleManager.CheckPosition();
    }
    
    public bool isCloseToInitialPosition()
    {
        var distance = Vector2.Distance(transform.position, respawn.GetInitialPosition());  
        return distance < 0.4f;
    }
}
