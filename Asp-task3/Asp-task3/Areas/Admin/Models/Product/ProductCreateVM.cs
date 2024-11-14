﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Asp_task3.Areas.Admin.Models.Shop
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Please enter the product title.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The title must be at least 3 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Product size is required.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Please provide at least 1 character for size.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Please provide a photo path.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "The photo path must be at least 5 characters long.")]
        public string PhotoPath { get; set; }

 

    }
}

