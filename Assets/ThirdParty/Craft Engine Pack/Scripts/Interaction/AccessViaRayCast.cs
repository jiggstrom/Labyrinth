using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccessViaRayCast : MonoBehaviour {

    public Camera m_camera;
    public float m_maxDistance = 1.0f; // max distance to see interactable objects
    public float m_rayWidth = 0.25f; // width of raycast
    public Text m_interactionHintText;// text to print hint
    public InputManager m_inputManager;

    InputUnit m_interactionKey;// for print hint
    InputUnit m_hitKey;// for print hint
    public InteractableObject m_currentObject { get; private set; }// object we see at the moment
    private InteractableObject m_prevCurObject;

    public HitableObject m_objectToHit { get; private set; }// hitable object we see at the moment
    Inventory m_inventory;
    void Start()
    {
        m_currentObject = null;
        m_objectToHit = null;
        m_interactionKey = m_inputManager.GetInputUnit(InputManager.InteractGroup, InputManager.Interaction);
        m_hitKey = m_inputManager.GetInputUnit(InputManager.InteractGroup, InputManager.InteractionHit);
        m_interactionHintText.text = "";
        m_inventory = GetComponent<Inventory>();
    }
    void Update()
    {
        //Ray ray = new Ray(m_camera.transform.position, m_camera.transform.forward);
        RaycastHit hit;

        if (!GameManager.instance.Canvas.LootScreen.activeInHierarchy && Physics.SphereCast(m_camera.transform.position, m_rayWidth, m_camera.transform.forward, out hit, m_maxDistance, 512)) // raycast forward from camera (only objects with "Visible" layer set)
        {
            m_objectToHit = hit.collider.gameObject.GetComponent<HitableObject>();
            m_currentObject = hit.collider.gameObject.GetComponent<InteractableObject>();

            if (m_currentObject == m_prevCurObject) return;
            if (m_prevCurObject) GameManager.instance.StopLookAt(m_prevCurObject);

            if (m_objectToHit && GameManager.instance.inventory.m_selectedToolCell?.m_item?.LootType == LootType.Tool && !m_currentObject.m_itemDescription.m_isLiftable)
            {

                 if ((GameManager.instance.inventory.m_selectedToolCell?.m_item?.LootType == LootType.Tool && (m_objectToHit.effectiveTools?.Length ?? 0) == 0) || m_objectToHit.effectiveTools.Contains(GameManager.instance.inventory.m_selectedToolCell?.m_item)) {
                    m_interactionHintText.text = $" Click {m_hitKey.Key} to hit {m_objectToHit.m_itemDescription.name}";
                    GameManager.instance.LookAt(m_currentObject);
                }
            }
            else if (!hit.collider.isTrigger && m_currentObject && m_currentObject.m_itemDescription.m_isLiftable)
            {
                //print interaction key and hint to interact
                m_interactionHintText.text = $"{ m_interactionKey.Key} to pick up {m_currentObject.m_itemDescription.m_name}";
                GameManager.instance.LookAt(m_currentObject);
            }
            else if (!hit.collider.isTrigger && m_currentObject && m_currentObject.m_itemDescription.m_isInteractable)
            {
                //print interaction key and hint to interact
                m_interactionHintText.text = $"{ m_interactionKey.Key} to {m_currentObject.m_itemDescription.InteractVerb} {m_currentObject.m_itemDescription.m_name}";
                GameManager.instance.LookAt(m_currentObject);
            }
            else
            {
                GameManager.instance.StopLookAt(m_currentObject);
                m_interactionHintText.text = "";
            }
            m_prevCurObject = m_currentObject;
        }
        else
        {
            if(m_currentObject) GameManager.instance.StopLookAt(m_currentObject);
            m_interactionHintText.text = "";
            m_currentObject = null;
            m_prevCurObject = null;
            m_objectToHit = null;
        }
    }

    public void Interact()
    {
        if(m_currentObject)
        {
            GameManager.instance.InteractWith(m_currentObject);
            m_currentObject.Interact();
        }
    }
}
