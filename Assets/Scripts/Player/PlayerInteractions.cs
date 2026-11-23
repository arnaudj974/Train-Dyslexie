using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerInputs inputs;

    public List<NpcBehaviour> closeNpc = new List<NpcBehaviour>();

    void Start()
    {
        inputs = GetComponent<PlayerMovement>().GetInputs();
        inputs.Player.Interact.performed += OnInteract;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.TryGetComponent<NpcBehaviour>(out var res);

        if (res != null)
        {
            res.ActiveImage(true);
            res.isPlayerClose = true;
            closeNpc.Add(res);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        collider.TryGetComponent<NpcBehaviour>(out var res);

        if (res != null && closeNpc.Contains(res))
        {
            res.ActiveImage(false);
            res.isPlayerClose = false;
            closeNpc.Remove(res);
        }
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        if (closeNpc.Count > 0)
        {
            closeNpc[0].Interact();
        }
    }
}
