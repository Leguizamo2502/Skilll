using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entity.DTOs;
using Entity.Models;

namespace Business.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pizza, PizzaDto>().ReverseMap();
            CreateMap<Pizza, CreatePedidoDto>().ReverseMap();
            CreateMap<CreatePedidoDto, Pedido>().ReverseMap();


            CreateMap<Pedido, PedidoDto>()
    .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
    .ForMember(dest => dest.PizzaNombre, opt => opt.MapFrom(src => src.Pizza.Nombre));

            CreateMap<Cliente, ClienteDto>().ReverseMap();



        }
    }
}
