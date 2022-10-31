using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : CharaController
{
    private Animator animator = null;
    private Vector2 firstPos;
    public float maxWalkSec = 1;
    private bool isMoving = true;
    private float time = 0;
    private readonly string[] animNames = { "Right", "Left", "Up", "Down" };

    //動くまでの時間
    public float waitInterval = 3f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        //初期位置記憶
        firstPos = transform.position;
        //StartWalk();
    }

    // Update is called once per frame
    void Update()
    {
        //time += Time.deltaTime;

        //if (isMoving)
        //{
        //    //時間以上歩いたら
        //    if (time >= maxWalkSec)
        //    {
        //        //時間リセット
        //        time = 0;
        //        //動けなくする
        //        isMoving = false;
        //        //速度0
        //        SetMoveVelocity(Key.NONE);
        //    }
        //}
        //else
        //{
        //    //一定時間待ったら
        //    if (time >= waitInterval)
        //    {
        //        StartWalk();
        //    }
        //}
    }

    //private void StartWalk()
    //{
    //    //進む方向を決める
    //    var keyIndex = Random.Range(0, System.Enum.GetValues(typeof(Key)).Length - 1);
    //    var key = (Key)System.Enum.ToObject(typeof(Key), keyIndex);
    //    SetMoveVelocity(key);

    //    if (animator)
    //    {
    //        //アニメーション再生
    //        animator.Play(animNames[keyIndex]);
    //    }

    //    //時間リセット
    //    time = 0;
    //    //動けるようにする
    //    isMoving = true;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //壁にぶつかったら
    //    //動けなくする
    //    isMoving = false;
    //    //時間リセット
    //    time = 0;
    //    //速度0
    //    SetMoveVelocity(Key.NONE);
    //}
}
