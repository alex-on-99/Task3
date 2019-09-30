﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB
{
    interface IArticleCommunication
    {
        ICollection<Article> GetArticles();
        Article GetArticleById(int id);
    }
}