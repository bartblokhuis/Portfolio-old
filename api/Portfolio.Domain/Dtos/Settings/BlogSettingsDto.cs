namespace Portfolio.Domain.Dtos.Settings;

public class BlogSettingsDto
{
    #region Properties

    public bool IsSendEmailOnSubscribing { get; set; }

    public string EmailOnSubscribingSubjectTemplate { get; set; }

    public string EmailOnSubscribingTemplate { get; set; }

    public bool IsSendEmailOnPublishing { get; set; }

    public string EmailOnPublishingSubjectTemplate { get; set; }

    public string EmailOnPublishingTemplate { get; set; }

    public bool IsSendEmailOnCommentReply { get; set; }

    public string EmailOnCommentReplySubjectTemplate { get; set; }

    public string EmailOnCommentReplyTemplate { get; set; }

    #endregion
}
