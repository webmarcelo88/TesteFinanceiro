﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Financeiro.Util.Configuracao
{
    public class CustomConfiguration
    {
        public string UrlBaseAPI { get; set; }
        public List<EndPoints> EndPoints { get; set; }
    }

    public class EndPoints
    {
        public string ApiName { get; set; }
        public string ApiPath { get; set; }

    }
}
