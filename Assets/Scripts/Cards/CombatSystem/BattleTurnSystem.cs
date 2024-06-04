using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StackLandsLike.GameCore;
using VMFramework.Procedure;
using System;
using UnityEngine.Scripting;
using Sirenix.OdinInspector;
using StackLandsLike.UI;
using StackLandsLike.Cards;

[ManagerCreationProvider(nameof(GameManagerType.BattleManager))]
public class BattleTurnSystem : ManagerBehaviour<BattleTurnSystem>, IManagerBehaviour
{
    public static List<GameObject> battleUnits;           //所有参战对象的列表，它控制的是参战顺序
    public static List<GameObject> playerUnits;           //所有参战玩家的列表
    public static GameObject[] enemyUnits;            //所有参战敌人的列表
    private static GameObject[] remainingEnemyUnits;           //剩余参战对敌人的列表
    private static GameObject[] remainingPlayerUnits;           //剩余参战对玩家的列表

    [ShowInInspector]
    private static GameObject currentActUnit;          //当前行动的单位
    public static GameObject currentActUnitTarget;            //当前行动的单位的目标

    private static Vector3 currentActUnitInitialPosition;         //当前行动单位的初始位置
    private static Quaternion currentActUnitInitialRotation;         //当前行动单位的初始朝向
    private static Vector3 currentActUnitTargetPosition;           //当前行动单位目标的位置
    public static bool isUnitRunningToTarget = false;            //玩家是否为移动至目标状态
    public static bool isUnitRunningBack = false;          //玩家是否为移动回原点状态
    private static float distanceToTarget;         //当前行动单位到攻击目标的距离
    private static float distanceToInitial;         //当前行动单位到初始位置的距离
    public static float unitMoveSpeed = 1f;         //单位战斗中的移动速度
    private static Vector3 currentactUnitStopPosition;         //当前行动单位的移动后停下的位置

    public int attackData;            //伤害值

    public GameObject bloodText;           //保存血条预制体
    public bool BattleIsOver = false;      //战斗是否结束

   // public GameObject meat;          //死了掉落的肉
    //public GameObject bone;

    //public float battleAreaRadius;   //战斗半径
    /// <summary>
    /// 创建初始参战列表，存储参战单位
    /// </summary>


    public static void OnPostInit()
    {
        //创建参战列表
        battleUnits = new List<GameObject>();
        playerUnits = new List<GameObject>();
        enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        playerUnits.AddRange(GameObject.FindGameObjectsWithTag("PlayerUnit"));
        int humanIndex = 0;
        int enemyIndex = 0;
        while (humanIndex < playerUnits.Count || enemyIndex < enemyUnits.Length)
        {
            if (humanIndex < playerUnits.Count)
            {
                battleUnits.Add(playerUnits[humanIndex]);
                humanIndex++;
            }

            if (enemyIndex < enemyUnits.Length)
            {
                battleUnits.Add(enemyUnits[enemyIndex]);
                enemyIndex++;
            }
        }
        Debug.Log(playerUnits.Count);
        Debug.Log(enemyUnits.Length);
        //开始战斗
        ToBattle();
  
    }

    public static void checkDead(List<GameObject> lists)
    {
        for(int i=0;i<lists.Count;i++)
        {
            if(lists[i].TryGetComponent(out CardGroup cardgroup)==false)
            {
                Debug.LogError("lists !!!");
                continue;
            }
            UnitStats currentActUnitStats = lists[i].GetComponent<UnitStats>();
            if (currentActUnitStats.IsDead())
            {
                CardGroupManager.DestroyCardGroup(cardgroup);
                BattleUIManage.blood.Remove(cardgroup.gameObject);
                if(BattleUIManage.blood.TryGetValue(cardgroup.gameObject,out var bloodbar))
                {
                    Destroy(bloodbar);
                }
               
            }
        }
    }

    /// <summary>
    /// 判断战斗进行的条件是否满足，取出参战列表第一单位，并从列表移除该单位，单位行动
    /// 行动完后重新添加单位至队列，继续ToBattle()
    /// </summary>
    public static void ToBattle()
    {
        remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");

        for (int i=0;i<playerUnits.Count;i++)
        {
            if (BattleUIManage.blood.TryGetValue(playerUnits[i] ,out var blood))
            {
                blood.SetActive(true);
            }
        }

        //检查存活敌人单位
        if (remainingEnemyUnits.Length == 0)
        {
            Debug.Log("敌人全灭，战斗胜利");
            for (int i = 0; i < playerUnits.Count; i++)
            {
                if (BattleUIManage.blood.TryGetValue(playerUnits[i], out var blood))
                {
                    blood.SetActive(false);
                }
            }
            checkDead(battleUnits);


        }
        //检查存活玩家单位
        else if (remainingPlayerUnits.Length == 0)
        {
            Debug.Log("我方全灭，战斗失败");
            checkDead(battleUnits);
        }
        else
        {
            //取出参战列表第一单位，并从列表移除
            currentActUnit = battleUnits[0];
            battleUnits.Remove(currentActUnit);
            //重新将单位添加至参战列表末尾
            battleUnits.Add(currentActUnit);

            //获取该行动单位的属性组件
            UnitStats currentActUnitStats = currentActUnit.GetComponent<UnitStats>();

            //判断取出的战斗单位是否存活
            if (!currentActUnitStats.IsDead())//没死
            { 
                //选取攻击目标
                Debug.Log("当前攻击者：" + currentActUnit.name);
                FindTarget();
            }
            else
            {
                GameObject blood = BattleUIManage.blood[currentActUnit];
                Destroy(blood);               
                //currentActUnit.SetActive(false);
                //  Drops(currentActUnit);//死了就掉落东西
                Debug.Log("目标死亡，跳过回合");
                ToBattle();
            }
        }
    }  


    /// <summary>
    /// 查找攻击目标，如果行动者是怪物则从剩余玩家中随机
    /// </summary>
    public static void FindTarget()
    {
        //如果行动单位是怪物则从存活玩家对象中随机一个目标
        if (currentActUnit.tag == "EnemyUnit")
        {
            int targetIndex = UnityEngine.Random.Range(0, remainingPlayerUnits.Length);
            currentActUnitTarget = remainingPlayerUnits[targetIndex];
        }
        else if (currentActUnit.tag == "PlayerUnit")
        {
            int targetIndex = UnityEngine.Random.Range(0, remainingEnemyUnits.Length);
            currentActUnitTarget = remainingEnemyUnits[targetIndex];
        }
        RunToTarget();
   

    }

    /// <summary>
    /// 攻击者移动到攻击目标前
    /// </summary>
    public static void RunToTarget()
    {
        currentActUnitInitialPosition = currentActUnit.transform.position;
        currentActUnitInitialRotation = currentActUnit.transform.rotation;         //保存移动前的位置和朝向，因为跑回来还要用
        currentActUnitTargetPosition = currentActUnitTarget.transform.position;         //目标的位置
        //开启移动状态
        isUnitRunningToTarget = true;
        //移动的控制放到Update里，因为要每一帧判断离目标的距离
    }

   

    /// <summary>
    /// 用户控制玩家选择目标状态的开启
    /// </summary>
    void Update()
    {
       
            if (isUnitRunningToTarget)
            {
               // currentActUnit.transform.LookAt(currentActUnitTargetPosition);           //单位移动的朝向
                 distanceToTarget = Vector3.Distance(currentActUnitTargetPosition, currentActUnit.transform.position);           //到目标的距离，需要实时计算                                                                                                               //避免靠近目标时抖动
                if (distanceToTarget > 1)
                {
                 currentActUnit.transform.position = currentActUnitTargetPosition;
                //currentActUnit.transform.Translate(Vector3.forward * unitMoveSpeed * Time.deltaTime, Space.Self);         //Time.deltaTime保证速度单位是每秒
                }
                else
                {
                    isUnitRunningToTarget = false;
                    //记录停下的位置
                    currentactUnitStopPosition = currentActUnit.transform.position;
                    //开始攻击
                    LaunchAttack();
                }
            }

            if (isUnitRunningBack)
            {
               // currentActUnit.transform.LookAt(currentActUnitInitialPosition);            //回来的朝向
                distanceToInitial = Vector3.Distance(currentActUnit.transform.position, currentActUnitInitialPosition);           //离初始位置的距离
                if (distanceToInitial > 1)
                {
                //  currentActUnit.transform.Translate(Vector3.forward * unitMoveSpeed * Time.deltaTime, Space.Self);         //Time.deltaTime保证速度单位是每秒
                currentActUnit.transform.position = currentActUnitInitialPosition;
            }
                else
                {                   
                    //关闭移动状态
                    isUnitRunningBack = false;
                    //修正到初始位置和朝向
                    currentActUnit.transform.position = currentActUnitInitialPosition;
                    currentActUnit.transform.rotation = currentActUnitInitialRotation;

                    //攻击单位回原位后行动结束，到下一个单位
                    ToBattle();
                }
            }
        
    }

    /// <summary>
    /// 当前行动单位执行攻击动作
    /// </summary>
    public void LaunchAttack()
    {
        //存储攻击者和攻击目标的属性脚本
        UnitStats attackOwner = currentActUnit.GetComponent<UnitStats>();
        UnitStats attackReceiver = currentActUnitTarget.GetComponent<UnitStats>();
        //根据攻防计算伤害
        attackData = attackOwner.attack - attackReceiver.defense  ;

        float damageAnimationTime = 1f;
        float attackAnimationTime = 1f;
        StartCoroutine(WaitForTakeDamage(damageAnimationTime));

        //下一个单位行动前延时
        StartCoroutine(WaitForRunBack(attackAnimationTime));
    }

    /// <summary>
    /// 延时操作函数，避免在怪物回合操作过快
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForTakeDamage(float time)
    {
        yield return new WaitForSeconds(time);

        //被攻击者承受伤害
        currentActUnitTarget.GetComponent<UnitStats>().ReceiveDamage(attackData);

        //实例化伤害字体并设置到画布上（字体位置和内容的控制放在它自身的脚本中）
        GameObject thisText = Instantiate(bloodText) as GameObject;
        thisText.transform.SetParent(GameObject.Find("BloodTextGroup").transform, false);
    }

    IEnumerator WaitForRunBack(float time)
    {
        yield return new WaitForSeconds(time);       
        isUnitRunningBack = true;

    }
}