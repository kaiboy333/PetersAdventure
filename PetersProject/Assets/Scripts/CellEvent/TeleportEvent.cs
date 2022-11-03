using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportEvent : CellEvent
{
    //飛ぶシーンの名前
    [SerializeField] private string sceneName;
    //使うパネル
    [SerializeField] private Image panelImage;
    //プレイヤーのScript
    [SerializeField] private YushaController yushaController;

    public override void CallEvent()
    {
        //タスクマネージャーがあるなら
        if (eventTaskManager)
        {
            //動けなくする
            eventTaskManager.PushTask(new DoNowTask(() => { yushaController.canMove = false; }));
            //暗くする
            eventTaskManager.PushTask(new AlphaManager(panelImage, false));
            //シーン移動
            eventTaskManager.PushTask(new DoNowTask(() => { SceneManager.LoadScene(sceneName); }));
        }
    }
}
