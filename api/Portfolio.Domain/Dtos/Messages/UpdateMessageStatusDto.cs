using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Dtos.Messages;

public class UpdateMessageStatusDto
{
    #region Properties

    public int Id { get; set; }

    public MessageStatus MessageStatus { get; set; }

    #endregion
}
