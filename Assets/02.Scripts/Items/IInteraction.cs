using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    string GetInteractPrompt();
    void OnInteract();
    string GetInteratHint();
}
