using UnityEngine;
using UnityEngine.UI;

public class AlcoholStatusChanger : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    [SerializeField] PlayerStatus playerStatus;

    private void Start()
    {

        foreach (var button in buttons) { button.onClick.AddListener(() => { playerStatus.IsServed(button.name);});}
    }
}
