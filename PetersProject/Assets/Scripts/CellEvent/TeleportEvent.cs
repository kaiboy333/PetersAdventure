using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportEvent : CellEvent
{
    //飛ぶシーンの名前
    [SerializeField] private string sceneName;
    //パネルのタグ名
    [SerializeField] private string panelTag = "BlackPanel";
    //使うパネル
    private Image panelImage;
    //プレイヤーのScript
    private YushaController yushaController;

    protected override void Start()
    {
        base.Start();

        panelImage = GameObject.FindWithTag(panelTag).GetComponent<Image>();

        yushaController = FindObjectOfType<YushaController>();
    }

    public override void CallEvent()
    {
        //タスクマネージャーがあるなら
        if (eventTaskManager && panelImage && yushaController)
        {
            //動けなくする
            eventTaskManager.PushTask(new DoNowTask(() => { yushaController.canMove = false; }));
            //yushaController.canMove = false;
            //暗くする
            eventTaskManager.PushTask(new AlphaManager(panelImage, false));
            //シーン移動
            eventTaskManager.PushTask(new DoNowTask(() => { SceneManager.LoadScene(sceneName); }));
        }
    }
}
