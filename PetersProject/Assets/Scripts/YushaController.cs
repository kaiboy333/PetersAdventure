using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YushaController : CharaController
{
    private Animator animator = null;

    private bool isMoving = false;

    private readonly List<Key> keys = new List<Key>();

    private Vector2 targetPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        targetPos = transform.position;
    }

    // Update is called once per frame
    protected void Update()
    {
        //base.Update();

        if (Input.GetKeyDown(KeyCode.D))
        {
            keys.Add(Key.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            keys.Add(Key.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            keys.Add(Key.UP);
        }
        else if (Input.GetKeyDown(KeyCode.S))
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

        if (!isMoving)
        {
            if (keys.Count == 0)
            {
                //キーを削除
                keys.Clear();
                return;
            }

            //最後に押しているキーを見て移動
            var direction = directions[(int)keys[keys.Count - 1]];
            targetPos = (Vector2)transform.position + direction * moveDistance;
            //歩けるなら
            if (CanWalk(targetPos))
            {
                StartCoroutine(MoveToward(targetPos));
            }

            //アニメーション再生
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }
    }

    IEnumerator MoveToward(Vector2 targetPos)
    {
        isMoving = true;

        //距離があるなら
        while (Vector2.Distance(targetPos, transform.position) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    //private void OnDrawGizmos()
    //{
    //    if (boxCollider2D)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(targetPos, moveDistance / 4.0f);
    //    }
    //}
}
