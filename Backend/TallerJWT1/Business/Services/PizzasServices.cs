﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Business.Repository;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;

namespace Business.Business
{
    public class PizzasServices : BusinessBasic<PizzaDto, PizzaDto,Pizza>, IPizzasService
    {
        public PizzasServices(IData<Pizza> data, IMapper mapper) : base(data, mapper)
        {
        }

        protected override void ValidateDto(PizzaDto dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("El nombre de la pizza es obligatorio.");

            if (dto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");
        }

        protected override async Task ValidateIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero");

            var entity = await Data.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("No se encontró la entidad con ese ID");
        }
    }
}
