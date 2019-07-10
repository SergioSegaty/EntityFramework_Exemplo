﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exemplo01.Model
{
    class Task
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }


        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}
