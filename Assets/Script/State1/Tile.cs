using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public static Tile instance;

    public static Tile selected;
    private SpriteRenderer Renderer;

    public Vector2Int Position;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        Renderer.color = Color.gray;
    }

    public void Unselect()
    {
        Renderer.color = Color.white;
    }

    public void OnMouseDown()
    {
        if (selected != null)
        {
            if (selected == this)
                return;
            selected.Unselect();
            if (Vector2Int.Distance(selected.Position, Position) == 1)
            {
                GridManager.Instance.SwapTiles(Position, selected.Position);
                selected = null;
            }
            else
            {
               ;
                selected = this;
                Select();
            }
        }
        else
        {
           
            selected = this;
            Select();
        }

       /* if (UIManager.instance.gameWinPanel.activeSelf)
        {
            selected.Unselect();
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
