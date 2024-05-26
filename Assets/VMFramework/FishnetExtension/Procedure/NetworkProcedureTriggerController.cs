#if FISHNET

using UnityEngine;

namespace VMFramework.Procedure
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public class NetworkProcedureTriggerController : NetworkManagerBehaviour<NetworkProcedureTriggerController>
    {
        public override void OnStartServer()
        {
            base.OnStartServer();

            Debug.Log("启动服务器");
            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ServerLoadingProcedure.ID);
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            Debug.Log("启动客户端");
            ProcedureManager.AddToSwitchQueue(MainMenuProcedure.ID, ClientLoadingProcedure.ID);
        }

        public override void OnStopClient()
        {
            base.OnStopClient();

            ProcedureManager.ExitProcedure(ClientRunningProcedure.ID);

            if (IsServerStarted == false)
            {
                ProcedureManager.EnterProcedure(MainMenuProcedure.ID);
            }
        }

        public override void OnStopServer()
        {
            base.OnStopServer();

            ProcedureManager.ExitProcedure(ServerRunningProcedure.ID);

            if (IsClientStarted == false)
            {
                ProcedureManager.EnterProcedure(MainMenuProcedure.ID);
            }
        }
    }
}

#endif
