using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    [SerializeField] private Text text;
    //str全部
    private string[] strs;

    //一度に表示できる列
    private const int PRINT_MAX_ROW = 3;
    
    //通常版
    private const float NORMAL_PRINT_INTERVAL = 0.5f;
    //高速版
    private const float FAST_PRINT_INTERVAL = 0.1f;
    //一文字表示するのに空ける時間
    private float printInterval = NORMAL_PRINT_INTERVAL;

    private bool isPrinting = false;
    private bool isLastPrint = false;

    //今表示している一番上の列
    private int row = 0;

    //使用するLogEvent
    private LogEvent logEvent = null;

    // Start is called before the first frame update
    void Start()
    {
        //見えないように
        gameObject.SetActive(false);

        if (text)
        {
            //文字列リセット
            text.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!text)
            return;

        //スペースキーを押したら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //表示中ではないなら
            if (!isPrinting)
            {
                //最後の表示だったなら
                if (isLastPrint)
                {
                    //文字列リセット
                    text.text = "";

                    //見えないように
                    gameObject.SetActive(false);

                    //LogEventを終わり判定にする
                    logEvent.isFinished = true;
                }
                else
                {
                    //列を表示分ずらす
                    row += PRINT_MAX_ROW;

                    //表示
                    StartCoroutine(PrintStr());
                }
            }
            //表示中なら
            else
            {
                //表示を高速にする
                printInterval = FAST_PRINT_INTERVAL;
            }
        }
    }

    public void SetLogEvent(LogEvent logEvent)
    {
        this.logEvent = logEvent;

        //初期化
        row = 0;
        isPrinting = false;
        isLastPrint = false;

        //改行で分ける
        strs = logEvent.Str.Split('\n');

        //文字列リセット
        text.text = "";

        //見えるように
        gameObject.SetActive(true);

        //表示
        StartCoroutine(PrintStr());
    }

    IEnumerator PrintStr()
    {
        var str = "";
        var printRow = PRINT_MAX_ROW;

        //表示が最後なら
        if (row + PRINT_MAX_ROW >= strs.Length)
        {
            //表示列数を調整
            printRow = strs.Length - row;
            //最後boolをtrue
            isLastPrint = true;
        }

        //指定列から表示列分を足す
        for(var i = 0; i < printRow; i++)
        {
            str += strs[row + i];
        }

        isPrinting = true;
        //一文字ずつ表示
        for(var i = 0; i < str.Length; i++)
        {
            text.text += str[i];
            yield return new WaitForSeconds(printInterval);
        }
        //速度を通常に
        printInterval = NORMAL_PRINT_INTERVAL;
        isPrinting = false;
    }
}
