using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageFloatUp : MonoBehaviour {
    
    private BattleTurnSystem turnScript;            //回合控脚本的引用

    //受到伤害单位的位置
    private Vector3 takeDamageUnit3DPosition;
    private Vector2 takeDamageUnit2DPosition;

    void Start ()
    {
        turnScript = GameObject.Find("BattleManager").GetComponent<BattleTurnSystem>();         //查找引用
       
        //计算伤害者的3D位置并转化为屏幕2D位置
        takeDamageUnit3DPosition = BattleTurnSystem.currentActUnitTarget.transform.position + new Vector3(0,1,0);         //适当上移修正位置
        takeDamageUnit2DPosition = Camera.main.WorldToScreenPoint(takeDamageUnit3DPosition);
        gameObject.GetComponent<RectTransform>().position = takeDamageUnit2DPosition;
        //设置数字内容
        gameObject.GetComponent<TextMeshProUGUI>().text = "-" + turnScript.attackData;
        
        //延迟销毁自身
        StartCoroutine("WaitAndDestory");
    }

	void Update () {
        //向上漂浮控制
        gameObject.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 1);
    }

    //延迟销毁
    IEnumerator WaitAndDestory()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
