using AutoMapper;
using Mh.Api.Controllers;
using Mh.Api.ViewModels;
using Mh.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mh.Api.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Usuario, UsuarioViewModel>()
              
                .ReverseMap();
        }
    }
}
