﻿using HUTECHClassroom.Application.Comments.Commands.CreateComment;
using HUTECHClassroom.Application.Comments.Commands.DeleteComment;
using HUTECHClassroom.Application.Comments.Queries.GetComment;
using HUTECHClassroom.Application.Posts;
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
        var id = await _mediator.Send(command);
        var comment = await _mediator.Send(new GetCommentQuery(id));

        await Clients.Group(command.PostId.ToString())
            .SendAsync("ReceiveComment", comment);
    }

    public async Task DeleteComment(DeleteCommentCommand command)
    {
        var comment = await _mediator.Send(command);
        if (comment is not null && comment.Post is not null)
            await Clients.Group(comment.Post.Id.ToString())
            .SendAsync("DeleteComment", comment);
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
        var result = await _mediator.Send(new GetPostCommentsWithPaginationQuery(Guid.Parse(postId), new PostPaginationParams(pageNumber, pageSize)));
        await Clients.Caller.SendAsync("LoadComments", result.Items, new
        {
            pageIndex = result.PageIndex,
            pageSize = result.PageSize,
            count = result.Count,
            totalCount = result.TotalCount,
            totalPages = result.TotalPages,
            hasPreviousPage = result.HasPreviousPage,
            hasNextPage = result.HasNextPage
        });
    }
}
