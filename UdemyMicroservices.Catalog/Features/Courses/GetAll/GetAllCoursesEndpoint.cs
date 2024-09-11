﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UdemyMicroservices.Catalog.Repositories;
using UdemyMicroservices.Shared;

namespace UdemyMicroservices.Catalog.Features.Courses.GetAll
{
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    namespace UdemyMicroservices.Catalog.Features.Courses.GetAll
    {
        public class GetAllCoursesQuery : IRequest<ServiceResult<List<CourseDto>>>
        {
        }

        public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper)
            : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
        {
            public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request,
                CancellationToken cancellationToken)
            {
                var courses = await context.Courses
                    .ToListAsync(cancellationToken: cancellationToken);

                var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);


                foreach (var course in courses)
                {
                    course.Category = categories.First(x => x.Id == course.CategoryId);
                }


                var coursesListDtos = mapper.Map<List<CourseDto>>(courses);


                return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesListDtos);
            }
        }


        public static class GetAllCoursesEndpoint
        {
            public static void MapGetAllCoursesQueryEndpoint(this WebApplication app)
            {
                app.MapGet("/courses", async (IMediator mediator) =>
                    {
                        var response = await mediator.Send(new GetAllCoursesQuery());
                        return Results.Ok(response);
                    })
                    .WithName("GetAllCourses")
                    .Produces<List<CourseDto>>(StatusCodes.Status200OK)
                    .WithTags("Courses");
            }
        }
    }
}