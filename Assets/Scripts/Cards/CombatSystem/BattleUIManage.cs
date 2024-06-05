using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using VMFramework.Procedure;
using UnityEngine.Scripting;
using StackLandsLike.GameCore;

[ManagerCreationProvider(nameof(GameManagerType.BattleUIManager))]
public class BattleUIManage : ManagerBehaviour<BattleUIManage>, IManagerBehaviour
{

    //把做好的预制体赋给这个变量
    public  GameObject bloodBar;

    //玩家和怪物的数组
    private static GameObject[] playerUnits;
    private static GameObject[] enemyUnits;

    //统一调整血条的偏移量
    public float bloodXOffeset;
    public float bloodYOffeset;
    public float bloodZOffeset;
    public static Dictionary<GameObject, GameObject> blood=new();

     public static void OnInitComplete()
    {        
            //搜索所有参战的玩家对象，逐个创建血条
            playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
            foreach (GameObject playerUnit in playerUnits)
            {
                GameObject playerBloodBar = Instantiate(instance.bloodBar) as GameObject;
                //实例化后设置到正确的画布分组里
                playerBloodBar.transform.SetParent(GameObject.Find("BloodBarGroup").transform, false);

                //设置血条的主人，BloodUpdate将根据这个主人的状态来更新血条
                playerBloodBar.GetComponent<BloodUpdate>().owner = playerUnit;
                blood.Add(playerUnit, playerBloodBar);
                SetTag.personHash.Add(playerUnit);
                playerBloodBar.SetActive(false);
            }
        try
        {
            //搜索所有参战的怪物对象，逐个创建血条
            enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
            foreach (GameObject enemyUnit in enemyUnits)
            {
                GameObject enemyBloodBar = Instantiate(instance.bloodBar) as GameObject;
                enemyBloodBar.transform.SetParent(GameObject.Find("BloodBarGroup").transform, false);

                //设置血条的主人，BloodUpdate将根据这个主人的状态来更新血条
                enemyBloodBar.GetComponent<BloodUpdate>().owner = enemyUnit;
                blood.Add(enemyUnit, enemyBloodBar);
                enemyBloodBar.SetActive(false);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("异常：{0}", ex.Message);
        }

    }

  

}
