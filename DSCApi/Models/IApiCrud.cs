using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSCApi.Models
{
    interface IApiCrud<T, A, B>
    {
        string connection { get; }//conexion
        Task<IEnumerable<T>> Read(A id, B id2,int page, int quantity);//lee y pagina
        Task<long> Count();//cuenta registros de una tabla para paginar
        Task<bool> Create(T data);//agrega nuevo registro
        Task<bool> Update(T data);//actualiza registro
        Task<bool> Delete(A id);//elimina registro
    }
}
