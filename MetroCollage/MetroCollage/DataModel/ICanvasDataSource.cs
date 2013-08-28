using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCollage.DataModel
{
    public interface ICanvasDataSource
    {
        Task<bool> SaveCanvasProjectAsync(CanvasProject canvas);
        Task<CanvasProject> LoadCanvasProjectAsync(int id);
    }
}
