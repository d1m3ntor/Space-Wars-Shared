using Unity.Netcode;
using UnityEngine;

namespace Shared.Scripts
{
    public abstract class Player : NetworkBehaviour
    {
        protected NetworkVariable<Vector2> direction = new (default, 
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    }
}