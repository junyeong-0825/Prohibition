using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionManager : MonoBehaviour
{
    #region Singleton
    public static InteractionManager interactionInstance;

    private void Awake()
    {
        if (interactionInstance == null)
        {
            interactionInstance = this;
        }
        else if (interactionInstance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    string objectName;
    [SerializeField] GameObject[] panels;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] GameObject alcoholPanel;
    private Dictionary<string, GameObject> storePanels = new Dictionary<string, GameObject>();
    public InputAction eKeyAction; // Input Action을 Inspector에서 설정할 수 있도록 함

    private void OnEnable()
    {
        eKeyAction.Enable();
        eKeyAction.performed += Interaction; // E 키가 눌렸을 때 실행할 함수를 연결
    }

    private void OnDisable()
    {
        eKeyAction.Disable();
        eKeyAction.performed -= Interaction; // 연결 해제
    }

    private void Start()
    {
        foreach (var panel in panels)
        {
            panel.gameObject.SetActive(false);
            storePanels.Add(panel.name, panel);
        }
    }
    public void SetValue(string name)
    {
        objectName = name;
    }


    void Interaction(InputAction.CallbackContext context)
    {
        if (objectName != "None")
        {
            if (storePanels.TryGetValue(objectName + "Panel", out GameObject panel))
            {
                panel.SetActive(true);
            }
            else if (objectName == "Counter")
            {
                playerStatus.IsServed("Food");
            }
            else if (objectName == "Booze")
            {
                alcoholPanel.SetActive(true);
            }
        }
    }
}
