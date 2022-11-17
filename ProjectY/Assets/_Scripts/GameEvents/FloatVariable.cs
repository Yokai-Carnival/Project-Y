using UnityEngine;

[CreateAssetMenu(fileName = "New Float Variable", menuName = "Scriptable Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [field:SerializeReference] public float Value { get; set; }
}
