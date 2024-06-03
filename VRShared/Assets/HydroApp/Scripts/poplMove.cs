using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poplMove : MonoBehaviour
{
    // public GameObject popl_up, popl_down; // верхний и нижний поплавки

    void Update()
    {
        transform.position += new Vector3(0, 0.0001f, 0);
    }


}

// Для верхнего поплавка
// при ЛАТР * 0.8 * ВН1 < 125

// Это для нижнего
// при ЛАТР * 0.8 * ВН1>= 125
// ((ЛАТР * ВН1) - 125) * 0.514

// Ниже 0 поплавок быть не должен 
// transform.position += new Vector3(1,0,0); // добавляем к X единицу
// transform.position -= new Vector3(1,1,0); // вычитаем из X и Y единицу
// transform.position = new Vector3(10,20,30); // устанавливаем объекту координаты 10,20,30 (x,y,z)