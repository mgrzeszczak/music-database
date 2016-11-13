﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Config
{
    public class DatabaseConfig
    {

        public static readonly string ConnectionString = "Server=localhost;Database=lyrics;User=root;Password=root;";
        public static readonly int DefaultAmountPerPage = 50;
        public static readonly int DefaultPageNumber = 1;

    }
}
