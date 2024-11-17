﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Universities.Infrastructure.Contracts.EntitiesDB
{
    public partial class WebPage
    {
        [Key]
        public int Id { get; set; }
        public int? UniversityId { get; set; }
        [Required]
        [StringLength(100)]
        public string WebPageName { get; set; }

        [ForeignKey("UniversityId")]
        [InverseProperty("WebPages")]
        public virtual University University { get; set; }
    }
}