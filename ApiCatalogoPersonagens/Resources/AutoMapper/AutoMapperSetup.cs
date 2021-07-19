using ApiCatalogoPersonagens.Entities;
using ApiCatalogoPersonagens.InputModel;
using ApiCatalogoPersonagens.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoPersonagens.Resources.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            CreateMap<PersonagemViewModel, Personagem>();
            CreateMap<Personagem, PersonagemViewModel>();

            CreateMap<PersonagemInputModel, Personagem>();
            CreateMap<Personagem, PersonagemInputModel>();
            //
        }
    }
}
