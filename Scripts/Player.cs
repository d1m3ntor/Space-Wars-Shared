using Unity.Netcode;
using UnityEngine;

namespace Shared.Scripts
{
    public class Player : NetworkBehaviour
    {
        private NetworkVariable<Vector2> direction = new (default, 
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        [ServerRpc]
        public void SetDirection_ServerRpc(Vector2 direction)
        {
            this.direction.Value = direction;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            #if !IS_SERVER 
                if (IsClient) PlayerConfigurator.Instance.Configure(this);
            #endif
        }
        
        private float speed = 3;
    
        private void Update()
        {
            transform.Translate(direction.Value * speed * Time.deltaTime);
        }
    }
}