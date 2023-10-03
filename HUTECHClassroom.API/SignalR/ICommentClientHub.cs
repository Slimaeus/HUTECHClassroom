namespace HUTECHClassroom.API.SignalR;

public interface ICommentClientHub
{
    Task ReceiveComment(CommentDTO comment);
    Task DeleteComment(CommentDTO comment);
    Task LoadComments(IList<CommentDTO> comments, object @params);
}
