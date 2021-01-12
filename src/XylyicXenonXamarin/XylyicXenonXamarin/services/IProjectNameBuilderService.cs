﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XylyicXenonXamarin.interfaces;

namespace XylyicXenonXamarin.services
{
    public interface IProjectNameBuilderService
    {
        Task<string> GetProjectName(INameOptions options = null);
    }
}
