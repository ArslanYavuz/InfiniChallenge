using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Interfaces
{
    public interface IBaseService
    {
        Task<T> GetAsync<T>(params object[] parameters);
        Task<T> PostAsync<T>(params object[] parameters);

    }
}
