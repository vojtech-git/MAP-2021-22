public delegate void InputEvent();
public delegate void InputEventFloat(float value);
public delegate void InputEventVector(float x, float y, float z);

public interface IController 
{
    event InputEventFloat ForwardEvent;
    event InputEventVector TurnEvent;

    event InputEvent FireEvent;
}
