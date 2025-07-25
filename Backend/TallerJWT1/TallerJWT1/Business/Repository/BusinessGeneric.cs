using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Interfaces;

namespace Business.Repository
{
    public abstract class BusinessGeneric<TDtoGet,TDto,TEntity> : BusinessBasic<TDto,TEntity> where TEntity : class
    {
        public BusinessGeneric(IData<TEntity> data,IMapper mapper) : base(data, mapper)
        {
        }
    }
}
