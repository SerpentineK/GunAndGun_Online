using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �l�ɐ����lint�ƃv���C���[������ł���"X"����肤��N���X����낤�Ƃ��āAint����̌p�����s�\�ƒm��f�O�B
// ����ɁAstring��int�̃v���p�e�B��������N���X��������B
public class ExtendedValue
{
    private int _myValue;
    private bool _isX;
    private bool isDetermined = false;

    public string MyStringValue 
    {
        get 
        {
            if (_isX && !isDetermined)
            {
                return "X";
            }
            else
            {
                return $"{_myValue:00}";
            }
        }

        set
        {
            if (value == "X")
            {
                _myValue = 0;
                _isX = true;
            }
            else if (int.TryParse(value, out int result))
            {
                _myValue = result;
                _isX = false;
            }
            else
            {
                return;
            }
        } 
    }

    public int MyIntValue 
    {
        get
        {
            if (_isX && !isDetermined)
            {
                return 0;
            }
            else
            {
                return _myValue;
            }
        }

        set
        {
            _myValue = value;
            _isX = false;
        }
    }

    public ExtendedValue() { return; }

    public ExtendedValue(string myValue)
    {
        MyStringValue = myValue;
    }

    public ExtendedValue(int myValue)
    {
        MyIntValue = myValue;
    }

    public void DetermineX(int intInput)
    {
        if (_isX && !isDetermined) 
        { 
            _myValue = intInput;
            isDetermined = true;
        }
    }
}
