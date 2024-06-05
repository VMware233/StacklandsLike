using StackLandsLike.GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using VMFramework.Procedure;

//[GameInitializerRegister(ServerRunningProcedure.ID, ProcedureLoadingType.OnEnter)]
//[Preserve]

public class BloodUpdate : MonoBehaviour
{
    //血条的主人，在创建时会通过BattleUIManager赋值
    public  GameObject owner;

    //血条的长度数值，1为满格
    private Image ownerBloodFill;

    //获取UI控制脚本的引用，要从它那里获取统一的偏移量
    private static BattleUIManage uiManager;

    //主人的3D空间位置
    private Vector3 playerBlood3DPosition;
    //将主人的3D位置映射到屏幕之后的2D位置
    private Vector2 playerBlood2DPosition;

    public static void OnInitComplete()
    {
        Debug.LogError("aaaaaaaaa!");
        //获取UI控制脚本的引用
        Transform parentTransform = GameObject.Find("^Core").transform;
        uiManager = parentTransform.Find("BattleUIManager").GetComponent<BattleUIManage>();
       
    }

    void Update()
    {
        if(!GameStateManager.isGameRunning)
        {
            return;
        }
        if (owner.tag == "PlayerUnit" || owner.tag == "EnemyUnit")
        {
            //更新血条长度
            ownerBloodFill = gameObject.transform.Find("BloodFill").GetComponent<Image>();
            ownerBloodFill.fillAmount = owner.GetComponent<UnitStats>().bloodPercent;           //bloodPercent在每个单位的UnitStats脚本中存储
            
            //更新血条位置
            //获取当前主人的空间位置，然后转换为2D屏幕位置
            try
            {
                playerBlood3DPosition = owner.transform.position + new Vector3(uiManager.bloodXOffeset, uiManager.bloodYOffeset, uiManager.bloodZOffeset);
                playerBlood2DPosition = Camera.main.WorldToScreenPoint(playerBlood3DPosition);
                gameObject.GetComponent<RectTransform>().position = playerBlood2DPosition;
            }
            catch(Exception ex)
            {
                Console.WriteLine("异常：{0}",ex.Message);
            }
           
        }

        //如果主人死了，则设置为未激活状态
        if (owner.GetComponent<UnitStats>().IsDead())
        {
            gameObject.SetActive(false);
        }
    }

}
