using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesApp.Services
{
    public interface ISeriesService
    {
        Task<List<T>> GetAll<T>(string endpoint) where T : class;
        Task<T> Get<T>(string endpoint, int id) where T : class;
        Task<bool> Add<T>(string endpoint, T s);
    }
}
