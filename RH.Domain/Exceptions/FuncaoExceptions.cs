﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Domain.Exceptions
{
    public class FuncaoJaExisteException : Exception
    {
        public FuncaoJaExisteException() : base("Esta função já existe!")
        { }
    }
}
