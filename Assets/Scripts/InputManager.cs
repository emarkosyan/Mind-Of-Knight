using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs
{
    KEY_MOVE_UP,
    KEY_MOVE_DOWN,
    KEY_MOVE_RIGHT,
    KEY_MOVE_LEFT,

    KEY_ATTACK
}
public class InputManager : MonoBehaviour
{
    private static InputManager IM;
    /*public KeyCode playerUp { get; set; }
    public KeyCode playerDown { get; set; }
    public KeyCode playerLeft { get; set; }
    public KeyCode playerRight { get; set; }*/

    public Dictionary<Inputs, KeyCode> BindKeys = new Dictionary<Inputs, KeyCode>();
    private KeyCode[] AllInputs;

    public void SetDefaultKeys()
    {
        BindKeys.Clear();

        BindKeys.Add(Inputs.KEY_MOVE_UP,      KeyCode.W);
        BindKeys.Add(Inputs.KEY_MOVE_DOWN,    KeyCode.S);
        BindKeys.Add(Inputs.KEY_MOVE_LEFT,    KeyCode.A);
        BindKeys.Add(Inputs.KEY_MOVE_RIGHT,   KeyCode.D);
    }

    void SetCustomKeys(Inputs inputKey, KeyCode key)
    {
        if (!BindKeys.ContainsKey(inputKey))
        {
            BindKeys.Add(inputKey, key);
        }
        else
        {
            BindKeys[inputKey] = key;
        }
    }

    public bool IsKeyChanged(Inputs inputKey)
    {
        if (Input.anyKey)
        {
            foreach(KeyCode k in AllInputs)
            {
                if (Input.GetKeyDown(k))
                {
                    SetCustomKeys(inputKey, k);
                    return true;
                }
            }
        }
        return false;
    }

    private void Awake()
    {
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
            Destroy(gameObject);

        AllInputs = System.Enum.GetValues(typeof(KeyCode)) as KeyCode[];
        
    }

}
