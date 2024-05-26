using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [Serializable]
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    public class ContainerChooser : BaseConfig, IEnumerable<Transform>
    {
        [LabelText("容器ID")]
        [IsNotNullOrEmpty]
        [SerializeField]
        private string containerID;

        [LabelText("容器")]
        [SerializeField]
        private Transform container;

        [Button("测试获取容器", ButtonStyle.Box)]
        public Transform GetContainer()
        {
            if (containerID.IsNullOrEmpty())
            {
                Debug.LogError("Container ID is null or empty.");
                return null;
            }
            
            if (container == null)
            {
                container = containerID.FindOrCreateGameObject().transform;
            }

            return container;
        }

        public Transform GetContainer(Transform parent)
        {
            var container = GetContainer();

            container.SetParent(parent);

            return container;
        }

        public void SetDefaultContainerID(string defaultContainerID)
        {
            if (containerID.IsNullOrEmptyAfterTrim())
            {
                containerID = defaultContainerID;
            }
        }

        public static implicit operator Transform(ContainerChooser chooser)
        {
            return chooser.GetContainer();
        }

        #region Enumerator

        public IEnumerator<Transform> GetEnumerator()
        {
            return container.Cast<Transform>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
