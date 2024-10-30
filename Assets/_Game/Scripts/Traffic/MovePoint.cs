using UnityEngine;

public class MovePoint : MonoBehaviour
{
    public bool IsEmpty {  get; private set; }

    public void Take()
    {
        IsEmpty = false;
    }

    public void Exempt()
    {
        IsEmpty = true;
    }
}
