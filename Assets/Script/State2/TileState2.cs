using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileState2 : MonoBehaviour
{
    public static TileState2 instance;

    public static TileState2 selected;
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

    void OnMouseEnter()
    {
        if (selected != null)
        {

            if (selected == this)
                return;

            selected.Unselect();
            if (Vector2Int.Distance(selected.Position, Position) == 1)
            {
                GridManagerState2.Instance.SwapTiles(Position, selected.Position);
                selected = null;
            }
            else if (true)
            {

            }
            else
            {
                selected = this;
                Select();
            }
        }
    }

    public void OnMouseDown()
    {
        if (selected == null)
        {
            selected = this;
            Select();
        }
    }


}
