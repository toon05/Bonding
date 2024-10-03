using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRollGenerator
{
    string GeneratePrompt(int gl); 
    void AddAttribute(string attribute);
}
