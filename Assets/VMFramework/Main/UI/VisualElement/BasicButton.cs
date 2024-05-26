using System;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class BasicButton : BasicVisualElement
    {
        public static readonly string ussClassName = "unity-button";
        private Clickable m_Clickable;

        /// <summary>
        ///        <para>
        /// Clickable MouseManipulator for this Button.
        /// </para>
        ///      </summary>
        public Clickable clickable
        {
            get => m_Clickable;
            set
            {
                if (m_Clickable != null && m_Clickable.target == this)
                    this.RemoveManipulator(m_Clickable);
                m_Clickable = value;
                if (m_Clickable == null)
                    return;
                this.AddManipulator(m_Clickable);
            }
        }

        public event Action clicked
        {
            add
            {
                if (m_Clickable == null)
                    clickable = new Clickable(value);
                else
                    m_Clickable.clicked += value;
            }
            remove
            {
                if (m_Clickable == null)
                    return;
                m_Clickable.clicked -= value;
            }
        }

        public BasicButton()
            : this(null)
        {

        }

        public BasicButton(Action clickEvent)
        {
            AddToClassList(Button.ussClassName);
            clickable = new Clickable(clickEvent);
            focusable = true;
            tabIndex = 0;
        }

        /// <summary>
        ///        <para>
        /// Instantiates a Button using data from a UXML file.
        /// </para>
        ///      </summary>
        public new class
            UxmlFactory : UxmlFactory<BasicButton, UxmlTraits>
        {
        }

        /// <summary>
        ///        <para>
        /// Defines UxmlTraits for the Button.
        /// </para>
        ///      </summary>
        public new class UxmlTraits : BasicVisualElement.UxmlTraits
        {
            /// <summary>
            ///        <para>
            /// Constructor.
            /// </para>
            ///      </summary>
            public UxmlTraits() : base() => focusable.defaultValue = true;
        }
    }
}
