using StackLandsLike.Cards;
using StackLandsLike.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

public class SetTag : MonoBehaviour
{
    public static HashSet<GameObject> personHash=new ();
    public GameObject bloodBar;
    //public static event Action<CardGroup, ICardRecipe> OnRecipeCompleted; 
    void Start()
    {
        CardCraftManager.OnRecipeCompleted += SetPersonTag;
    }
    void SetPersonTag(CardGroup cardgroup1, ICardRecipe info)
    {  
        foreach(var cardgroup in CardGroupManager.GetActiveCardGroups())
        {
            foreach (var item in cardgroup.cards)
            {
                if (item is IPersonCard)
                {
                    //这里将给item对应的gameobject tag赋值为PlayerUnit
                    var cardview = CardViewManager.GetCardView(item);
                    cardview.gameObject.tag = "PlayerUnit";
                    cardview.AddComponent<UnitStats>().SetStates((ICreatureCard)cardview.card); ;
                    if(!personHash.Contains(cardview.gameObject))
                    {
                        personHash.Add(cardview.gameObject);
                        GameObject enemyBloodBar = Instantiate(bloodBar) as GameObject;
                        enemyBloodBar.transform.SetParent(GameObject.Find("BloodBarGroup").transform, false);
                        enemyBloodBar.GetComponent<BloodUpdate>().owner = cardgroup.gameObject;
                        BattleUIManage.blood.Add(cardgroup.gameObject, enemyBloodBar);
                        enemyBloodBar.SetActive(false);
                    }
                }
            }
        }
        
       
    }
}
