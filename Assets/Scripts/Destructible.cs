using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface Destructible
{
    public void TakeHit();
    public void SelfDestruct();
}