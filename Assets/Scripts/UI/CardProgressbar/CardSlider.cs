using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using UnityEngine;
using UnityEngine.UI;
using VMFramework.Containers;
using VMFramework.Core;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;
using VMFramework.Timers;

namespace StackLandsLike.Cards
{
    public class CardSlider : MonoBehaviour
    {

        public GameObject SliderPrefab;
        public float offset=0.1f;
        private void OnEnable()
        {
            EventManager.CardCompositionStarted += ProduceCardSlider;
            EventManager.ProgressBarTick += ChangeCardProgressBar;
            EventManager.StopComposition += StopCompositionHandle;
        }

        private void OnDisable()
        {
            EventManager.CardCompositionStarted -= ProduceCardSlider;
            EventManager.ProgressBarTick -= ChangeCardProgressBar;
            EventManager.StopComposition -= StopCompositionHandle;
        }
      

        public void ProduceCardSlider(object sender, CardCompositionEvent e)
        {       
            CardGroup cardGroup = (CardGroup)sender;
            Vector3 sliderPosition = new Vector3(cardGroup.transform.position.x, cardGroup.transform.position.y , 0);
            GameObject progressBar = Instantiate(SliderPrefab);
            Slider cardBar = progressBar.GetComponent<Slider>();
            Transform canvas = cardGroup.transform.Find("Canvas");
            progressBar.transform.SetParent(canvas.transform, false);
            progressBar.name = "slider1";
                    
            cardBar.GetComponent<RectTransform>().position=Camera.main.WorldToScreenPoint(sliderPosition);
            cardBar.minValue = 0;
            cardBar.maxValue = e.progress;
            Debug.Log("生成成功！");
        }
      
     
        public void ChangeCardProgressBar(object sender,ProgressBarTickEvent e) 
        {
            CardGroup cardGroup = (CardGroup)sender;
            Transform canvas = cardGroup.transform.Find("Canvas");
            Transform cardBarTransform = canvas.Find("slider1");                       
            Slider cardBar = cardBarTransform.GetComponent<Slider>();
            if (cardBar!=null)
            {
                cardBar.value = e.info.tick;               
            }           
            //Debug.Log("进度条在改变！");
            //  Debug.Log("cardBar.transform.position"+cardBar.transform.position);
            //   Debug.Log("e.info.tick:"+e.info.tick);

            // cardBar.value = (int)CardCraftManager.GetCraftingTicks(cardGroup);
        }
        public void StopCompositionHandle(StopCompositionEvent e)
        {
            Transform canvas = e.cardGroup.transform.Find("Canvas");
            Transform cardBarTransform = canvas.Find("slider1");
            Destroy(cardBarTransform.gameObject);   
            Debug.Log("进度条被摧毁！");
        }

    }

}

