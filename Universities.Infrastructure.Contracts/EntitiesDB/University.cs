﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Universities.Infrastructure.Contracts.EntitiesDB
{
    public partial class University
    {
        public University()
        {
            Domains = new HashSet<Domain>();
            WebPages = new HashSet<WebPage>();
        }

        [Key]
        public int Id { get; set; }
        public int? IdFromWebApi { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(2)]
        public string AlphaTwoCode { get; set; }
        [StringLength(50)]
        public string StateProvince { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [InverseProperty("University")]
        public virtual ICollection<Domain> Domains { get; set; }
        [InverseProperty("University")]
        public virtual ICollection<WebPage> WebPages { get; set; }
    }
}