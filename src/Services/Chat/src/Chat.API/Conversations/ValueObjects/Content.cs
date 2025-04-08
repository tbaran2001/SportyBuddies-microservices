namespace Chat.API.Conversations.ValueObjects;

public record Content
{
    public string Value { get; }

    [JsonConstructor]
    private Content(string value) => Value = value;

    public static Content Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidContentException(value);

        return new Content(value);
    }

    public static implicit operator string(Content content) => content.Value;
}