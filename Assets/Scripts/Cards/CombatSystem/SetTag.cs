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
        if (info.id == "person_craft_recipe")
        {
            Debug.LogError(info.id);
            foreach (var cardgroup in CardGroupManager.GetActiveCardGroups())
            {
                foreach (var item in cardgroup.cards)
                {
                    if (item is IPersonCard)
                    {
                        var cardview = CardViewManager.GetCardView(item);

                        if (!personHash.Contains(cardview.gameObject))
                        {
                            /*GameObject personCardGroup = cardview.transform.parent.gameObject;
                            personCardGroup.AddComponent<UnitStats>().SetStates((IPersonCard)cardview.card);  //这里把Icreature改为Iperson
                            personHash.Add(personCardGroup);
                            personCardGroup.tag= "PlayerUnit";*/

                            cardview.gameObject.tag = "PlayerUnit";
                            cardview.AddComponent<UnitStats>().SetStates((IPersonCard)cardview.card);
                            personHash.Add(cardview.gameObject);

                            if (BattleUIManage.blood.ContainsKey(cardview.gameObject) == false)
                            {
                                GameObject enemyBloodBar = Instantiate(bloodBar) as GameObject;
                                enemyBloodBar.transform.SetParent(GameObject.Find("BloodBarGroup").transform, false);
                                enemyBloodBar.GetComponent<BloodUpdate>().owner = cardview.gameObject;
                                BattleUIManage.blood.Add(cardview.gameObject, enemyBloodBar);
                                enemyBloodBar.SetActive(false);
                            }

                        }
                    }
                }

            }
        }
            
       
        
       
    }
}
