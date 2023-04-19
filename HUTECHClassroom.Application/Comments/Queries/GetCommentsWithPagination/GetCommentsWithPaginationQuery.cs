using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;

public record GetCommentsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<CommentDTO>(Params);
public class GetCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetCommentsWithPaginationQuery, CommentDTO>
{
    public GetCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Comment, bool>> SearchStringPredicate(string searchString) =>
        x => x.Content.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Comment, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

