using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    string GetInteractPrompt();
    void OnInteract();
}

public class InteractManager : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastChackTime;
    public float maxCheckDistance;
    public LayerMask interacrtable;


    private GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    public TextMeshProUGUI hintText;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastChackTime > checkRate)
        {
            lastChackTime = Time.time;

            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, interacrtable))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }

            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = string.Format("<b>[E]</b> {0}", curInteractable.GetInteractPrompt());
    }

    public void OnInteractInput(InputAction.CallbackContext callbackContext)
    {

        if (callbackContext.phase == InputActionPhase.Started && curInteractable != null)
        {
            if (gameObject.tag == "Item")
            {
                curInteractable.OnInteract();
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
            else if (gameObject.tag == "NeedHint")
            {
                curInteractable.OnInteract();
                curInteractGameObject = null;
                curInteractable = null;
                hintText.gameObject.SetActive(false);
            }
        }
    }
}
