namespace Portfolio.Domain.Dtos.Messages;

public class CreateMessageDto
{
    #region Properties

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string MessageContent { get; set; }

    #endregion
}
