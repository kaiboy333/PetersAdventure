using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CellEvent : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap = null;
    [SerializeField] protected EventTaskManager eventTaskManager = null;

    public enum CellType
    {
        ON,
        Check,
    }
    public CellType cellType = CellType.ON;

    protected void Start()
    {
        //タイルマップの位置調整
        var cellPos = tilemap.WorldToCell(transform.position);

        var complementPos = new Vector3(tilemap.cellSize.x / 2.0f, tilemap.cellSize.y / 2.0f, 0);

        transform.position = tilemap.CellToWorld(cellPos) + complementPos;
    }

    public abstract void CallEvent();
}
