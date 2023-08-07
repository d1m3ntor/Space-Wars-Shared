using Unity.Netcode;
using UnityEngine;

namespace Shared.Scripts
{
    public class Player : NetworkBehaviour
    {
        private Vector2 direction;

        [ServerRpc(RequireOwnership = false)]
        public void SetDirection_ServerRpc(Vector2 direction)
        {
            Debug.Log("OnMove");
            this.direction = direction;
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
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}