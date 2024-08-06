public class Event
{
    public string Content { get; }
    private InputType _inputType;

    public Event(string content, InputType inputType)
    {
        Content = content;
        _inputType = inputType;
    }

    public enum InputType
    {
        Text, Number
    }
}
