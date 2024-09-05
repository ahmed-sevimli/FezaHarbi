using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface Moveable
{
    public void Accelerate();
    public void StopMovement();
    public void Navigate();
    public void Recoil();
}