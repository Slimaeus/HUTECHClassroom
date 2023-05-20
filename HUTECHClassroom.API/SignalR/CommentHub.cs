using HUTECHClassroom.Application.Comments;
using HUTECHClassroom.Application.Comments.Commands.CreateComment;
using HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HUTECHClassroom.API.SignalR;

public class CommentHub : Hub
{
    private readonly IMediator _mediator;

    public CommentHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendComment(CreateCommentCommand command)
    {
        var comment = await _mediator.Send(command);

        await Clients.Group(command.PostId.ToString())
            .SendAsync("ReceiveComment", comment);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var postId = httpContext.Request.Query["postId"];
        var isParsePageNumberSuccess = int.TryParse(httpContext.Request.Query["pageNumber"], out int pageNumber);
        var isParsePageSizeSuccess = int.TryParse(httpContext.Request.Query["pageSize"], out int pageSize);
        if (!isParsePageNumberSuccess) pageNumber = 1;
        if (!isParsePageSizeSuccess) pageSize = 5;
        await Groups.AddToGroupAsync(Context.ConnectionId, postId);
        var result = await _mediator.Send(new GetPostCommentsWithPaginationQuery(Guid.Parse(postId), new CommentPaginationParams(pageNumber, pageSize)));
        await Clients.Caller.SendAsync("LoadComments", result);
    }
}
