using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Menu
{
    Food,
    Beer,
    Wine,
    Whisky
}


public class PlayerStatus : MonoBehaviour
{
    public bool isServed = false;
    Menu whatServed;


}
