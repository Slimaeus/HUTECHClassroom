using HUTECHClassroom.Domain.Constants.HttpParams;
using HUTECHClassroom.Domain.Constants.HttpParams.Common;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace HUTECHClassroom.API.SignalR;

public sealed class CommentHub : Hub<ICommentClientHub>
{
    private readonly IMediator _mediator;

    public CommentHub(IMediator mediator)
        => _mediator = mediator;

    public async Task SendComment(CreateCommentCommand command)
    {
        var id = await _mediator.Send(command);
        var comment = await _mediator.Send(new GetCommentQuery(id));

        await Clients.Group(command.PostId.ToString())
            .ReceiveComment(comment).ConfigureAwait(false);
    }

    public async Task DeleteComment(DeleteCommentCommand command)
    {
        var comment = await _mediator.Send(command);
        if (comment is not null && comment.Post is not null)
            await Clients.Group(comment.Post.Id.ToString())
                .DeleteComment(comment).ConfigureAwait(false);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext() ?? throw new NullReferenceException();
        var postId = httpContext.Request.Query[PostParamsConstants.POST_ID];
        if (postId is { }) return;
        var isParsePageNumberSuccess = int.TryParse(httpContext.Request.Query[PaginationParamsConstants.PAGE_NUMBER], out int pageNumber);
        var isParsePageSizeSuccess = int.TryParse(httpContext.Request.Query[PaginationParamsConstants.PAGE_SIZE], out int pageSize);
        if (!isParsePageNumberSuccess) pageNumber = 1;
        if (!isParsePageSizeSuccess) pageSize = 5;
        await Groups.AddToGroupAsync(Context.ConnectionId, postId).ConfigureAwait(false);
        var result = await _mediator.Send(new GetPostCommentsWithPaginationQuery(Guid.Parse(postId), new PostPaginationParams(pageNumber, pageSize)));
        await Clients.Caller
            .LoadComments(result.Items, new
            {
                pageIndex = result.PageIndex,
                pageSize = result.PageSize,
                count = result.Count,
                totalCount = result.TotalCount,
                totalPages = result.TotalPages,
                hasPreviousPage = result.HasPreviousPage,
                hasNextPage = result.HasNextPage
            }).ConfigureAwait(false);
    }
}
