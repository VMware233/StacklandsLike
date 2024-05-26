using System;

namespace VMFramework.Procedure
{
    public interface IInitializer
    {
        public void OnBeforeInit(Action onDone)
        {
            onDone();
        }
        
        public void OnPreInit(Action onDone)
        {
            onDone();
        }

        public void OnInit(Action onDone)
        {
            onDone();
        }

        public void OnPostInit(Action onDone)
        {
            onDone();
        }

        public void OnInitComplete(Action onDone)
        {
            onDone();
        }
    }
}
