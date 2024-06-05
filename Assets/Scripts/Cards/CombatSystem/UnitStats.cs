using StackLandsLike.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Property;

public class UnitStats : MonoBehaviour
{
    public  int health;
    public  int attack;
    public  int defense;
    public  float speed =5;

    public  int intialBlood;
    public  float bloodPercent;

    private bool dead = false;
    // Use this for initialization
    public void SetStates(ICreatureCard creature)
    {
        health = creature.health;
        attack = creature.attack;
        defense = creature.defense;
        intialBlood = health;
        bloodPercent = (float)health / intialBlood;

    }
      

    public void ReceiveDamage(int damage, CreatureCard creature)
    {
        health -= damage;
        bloodPercent = (float)health / intialBlood;
//        Debug.LogError(bloodPercent);
        if (health <= 0)
        {
            dead = true;
            gameObject.tag = "DeadUnit";
            Debug.Log(gameObject.name + "  is dead!");
        }
        Debug.Log(gameObject.name + "掉血" + damage + "点，剩余生命值" + health);
        creature.health.value = health;

    }

    public bool IsDead()
    {
        return dead;
    }


}
