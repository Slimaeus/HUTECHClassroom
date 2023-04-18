using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination
{
    public record GetClassroomsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ClassroomDTO>(Params);
    public class GetClassroomsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Classroom, GetClassroomsWithPaginationQuery, ClassroomDTO>
    {
        public GetClassroomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        protected override Expression<Func<Classroom, bool>> SearchStringPredicate(string searchString)
        {
            var toLowerSearchString = searchString.ToLower();
            return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
        }
        protected override Expression<Func<Classroom, object>> OrderByKeySelector()
        {
            return x => x.CreateDate;
        }
    }
}
