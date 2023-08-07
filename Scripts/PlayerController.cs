using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shared.Scripts
{
    public class PlayerController : NetworkBehaviour, PlayerActions.IDefaultMapActions
    {
        private float speed = 3;
        private NetworkVariable<Vector2> direction;
        private PlayerActions playerActions;

        private void OnEnable()
        {
            if (IsClient) playerActions.DefaultMap.Enable();
        }

        private void OnDisable()
        {
            if (IsClient) playerActions.DefaultMap.Disable();
        }
    
        private void Awake()
        {
            if (IsClient)
            {
                playerActions = new PlayerActions();
                playerActions.DefaultMap.AddCallbacks(this);
            }
        }

        private void Update()
        {
            if (IsServer) transform.parent.Translate(direction.Value * speed * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (IsClient) direction.Value = context.ReadValue<Vector2>();
        }
    }
}
