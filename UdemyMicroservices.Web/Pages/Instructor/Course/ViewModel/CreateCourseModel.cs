﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UdemyMicroservices.Web.Pages.Instructor.Course.ViewModel
{
    public record CreateCourseModel
    {
        public static CreateCourseModel Empty => new();


        [Display(Name = "Course Category")] public SelectList CategoryDropdownList { get; set; } = default!;


        [Display(Name = "Course Picture")] public IFormFile? PictureFormFile { get; init; }


        [Display(Name = "Course Name")] public string Name { get; init; } = default!;


        [Display(Name = "Course Description")] public string Description { get; init; } = default!;


        [Display(Name = "Course Price")] public decimal Price { get; init; }

        public Guid CategoryId { get; init; }
    }

    public record CategoryModel(Guid Id, string Name);
}