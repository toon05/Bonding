using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRollGenerator
{
    string GeneratePrompt(); 
    void AddAttribute(string attribute);
}
