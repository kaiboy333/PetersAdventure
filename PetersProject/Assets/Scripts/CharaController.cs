using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CharaController : MonoBehaviour
{
    public float speed = 2.5f;
    protected Key key;
    protected readonly Vector2[] directions = { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
    public float moveDistance;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask cellMask;
    protected BoxCollider2D boxCollider2D = null;
    [SerializeField] private Tilemap tilemap;

    [HideInInspector] public bool canMove = true;

    public enum Key
    {
        RIGHT,
        LEFT,
        UP,
        DOWN,
    }

    protected virtual void Start()
    {
        moveDistance = tilemap.cellSize.x;

        //タイルマップによる位置調整
        var cellPos = tilemap.WorldToCell(transform.position);

        var complementPos = new Vector3(tilemap.cellSize.x / 2.0f, tilemap.cellSize.y / 2.0f, 0);

        transform.position = tilemap.CellToWorld(cellPos) + complementPos;
    }

    //目的地まで歩けるか
    protected bool CanWalk(Vector2 targetPos)
    {
        var hitColliders = Physics2D.OverlapCircleAll(targetPos, moveDistance / 4.0f, hitMask);

        foreach(var hitCollider in hitColliders)
        {
            //当たったコライダーが自分のとは違うものなら
            if(hitCollider.gameObject != this.gameObject)
            {
                //壁がある
                return false;
            }
        }

        return true;
    }

    protected CellEvent GetCellEvent(Vector2 targetPos)
    {
        var hitColliders = Physics2D.OverlapCircleAll(targetPos, moveDistance / 4.0f, cellMask);

        foreach (var hitCollider in hitColliders)
        {
            //当たったコライダーが自分のとは違うものなら
            if (hitCollider.gameObject != this.gameObject)
            {
                //CellEventを取得
                var cellEvent = hitCollider.gameObject.GetComponent<CellEvent>();
                return cellEvent;
            }
        }

        return null;
    }
}
