﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}