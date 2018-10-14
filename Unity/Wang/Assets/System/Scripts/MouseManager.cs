using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorway;
    public Texture2D sword;

    public EventVector3 OnClickEnvironment;
    public EventVector3 OnRightClickEnvironment;
    public EventGameObject OnClickAttackable;

    private bool _useDefaultCursor = false;

    private void Awake()
    {
        if (null != GameManager.Instance)
            GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        _useDefaultCursor = (currentState != GameManager.GameState.RUNNING);
    }

    void Update()
    {
        if (_useDefaultCursor)
        {
            return;
        }

        // Raycast into scene
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            //Check if collision is attackable
            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                Cursor.SetCursor(sword, new Vector2(0, 0), CursorMode.Auto);
            }

            // If environment surface is clicked, invoke callbacks.
            if (Input.GetMouseButtonDown(0))
            {
                if (isAttackable)
                {
                    GameObject attackable = hit.collider.gameObject;
                    OnClickAttackable.Invoke(attackable);
                    return;
                }

                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position + doorway.forward * 10);
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                OnRightClickEnvironment.Invoke(hit.point);
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }

    }
}


[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

[System.Serializable]
public class EventGameObject : UnityEvent<GameObject> { }