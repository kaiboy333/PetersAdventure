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
    [SerializeField] private LayerMask mask;
    protected BoxCollider2D boxCollider2D = null;
    [SerializeField] private Tilemap tilemap;

    //private Vector2 beforePos;
    //[HideInInspector] public Vector2 nowPos;
    //[HideInInspector] public Vector2 deltaPos;

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

        var cellPos = tilemap.WorldToCell(transform.position);

        var complementPos = new Vector3(tilemap.cellSize.x / 2.0f, tilemap.cellSize.y / 2.0f, 0);

        transform.position = tilemap.CellToWorld(cellPos) + complementPos;

        ////現在の位置を記憶
        //nowPos = transform.position;
        //beforePos = nowPos;
    }

    //protected virtual void Update()
    //{
    //    //現在の位置を記憶
    //    nowPos = transform.position;
    //    deltaPos = (Vector2)transform.position - beforePos;
    //    //前回のを今のに
    //    beforePos = nowPos;
    //}

    //目的地まで歩けるか
    protected bool CanWalk(Vector2 targetPos)
    {
        var hitColliders = Physics2D.OverlapCircleAll(targetPos, moveDistance / 4.0f, mask);

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
}
