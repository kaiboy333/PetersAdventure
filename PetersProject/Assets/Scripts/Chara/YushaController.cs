using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YushaController : CharaController
{
    private Animator animator = null;

    private bool isMoving = false;

    private readonly List<Key> keys = new List<Key>();

    private Vector2 targetPos;

    //現れる位置
    public static Vector2 firstPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        //初期位置に移動
        transform.position = firstPos;
        //目標位置初期化
        targetPos = transform.position;

        //タイルによる位置修正
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        //base.Update();

        if (Input.GetKeyDown(KeyCode.D))
        {
            keys.Add(Key.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            keys.Add(Key.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            keys.Add(Key.UP);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            keys.Add(Key.DOWN);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            keys.Remove(Key.RIGHT);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            keys.Remove(Key.LEFT);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            keys.Remove(Key.UP);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            keys.Remove(Key.DOWN);
        }

        if (!canMove)
        {
            //Debug.Log("Can't Move");
            return;
        }

        //動いていないなら
        if (!isMoving)
        {
            //キーがないなら
            if (keys.Count == 0)
            {
                //終わり
                return;
            }

            //最後に押しているキーを見て移動
            var direction = directions[(int)keys[keys.Count - 1]];
            targetPos = (Vector2)transform.position + direction * moveDistance;
            //歩けるなら
            if (CanWalk(targetPos))
            {
                //trueに
                isMoving = true;
            }

            //アニメーション再生
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
        //動いているなら
        else
        {
            //移動
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            //たどり着いたら
            if(Vector2.Distance(targetPos, transform.position) < Mathf.Epsilon)
            {
                //false
                isMoving = false;

                //CellEventを取得
                var cellEvent = GetCellEvent(targetPos);
                //CellEventがあるなら
                if (cellEvent)
                {
                    //CellEventのタイプが乗るタイプなら
                    if (cellEvent.cellType == CellEvent.CellType.ON)
                    {
                        //イベントを呼ぶ
                        cellEvent.CallEvent();
                        Debug.Log("Call Event: " + targetPos);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (boxCollider2D)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPos, moveDistance / 4.0f);
        }
    }
}
